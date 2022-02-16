#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AtlasBlog.Data;
using AtlasBlog.Models;
using Microsoft.AspNetCore.Authorization;
using AtlasBlog.Services;
using AtlasBlog.Services.Interfaces;

namespace AtlasBlog.Controllers
{
    public class BlogPostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageService _imageService;
        private readonly SlugService _slugService;

        public BlogPostsController(ApplicationDbContext context, SlugService slugService, IImageService imageService)
        {
            _context = context;
            _slugService = slugService;
            _imageService = imageService;
        }

        // GET: BlogPosts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BlogPosts.Include(b => b.Blog);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BlogPosts/Details/5
        public async Task<IActionResult> Details(string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPosts
                .Include(b => b.Blog)
                .Include(c => c.Comments)
                .ThenInclude(c => c.Author)
                .FirstOrDefaultAsync(m => m.Slug== slug);

            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        //GET: BlogPosts/Create
        [Authorize(Roles = "Administrator, Moderator")]
        public IActionResult Create()
        {
            ViewData["BlogId"] = new SelectList(_context.Blogs, "Id", "BlogName");
            return View();
        }

        // POST: BlogPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BlogId,Title,Abstract,BlogPostState,Body")] BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                var slug = _slugService.UrlFriendly(blogPost.Title, 100);

                // HAVE TO ENSURE THE SLUG IS UNIQUE BEFORE ALLOW TO BE STORED IN THE DB  ------------>
                // IF YES TO UNIQUE, CAN BE USED. OTHERWISE WE HAVE TO THROW A CUSTOM ERROR LETTING USER KNOW WHAT HAPPENED
                var isUnique = !_context.BlogPosts.Any(blogpost => blogPost.Slug == slug);

                if (isUnique)
                {
                    blogPost.Slug = slug;
                }
                else
                {
                    // THE SLUG CANNOT BE USED AND AN ERROR MUST BE SHOWN TO THE USER
                    ModelState.AddModelError("Title", "Incorrect TItle (duplicate SLUG)");
                    ModelState.AddModelError("", "Incorrect TItle (duplicate SLUG)");
                    ViewData["BlogId"] = new SelectList(_context.Blogs, "Id", "BlogId", blogPost.BlogId);
                    return View(blogPost);
                }

                blogPost.Created = DateTime.UtcNow;

                _context.Add(blogPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            ViewData["BlogId"] = new SelectList(_context.Blogs, "Id", "BlogId", blogPost.BlogId);
            return View(blogPost);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        // GET: BlogPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost == null)

            {
                return NotFound();
            }

            ViewData["BlogId"] = new SelectList(_context.Blogs, "Id", "BlogName", blogPost.BlogId);
            return View(blogPost);
        }

        // POST: BlogPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BlogId,Title,Slug,IsDeleted,Abstract,BlogPostState,Body,Created")] BlogPost blogPost)
        {
            if (id != blogPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                try
                {
                    // IF THE SLUG HAS CHANGED I NEED TO DO THIS CHECK ------------------->
                    var slug = _slugService.UrlFriendly(blogPost.Title, 100);

                    if (blogPost.Slug != slug)
                    {
                        // HAVE TO ENSURE THE SLUG IS UNIQUE BEFORE ALLOW TO BE STORED IN THE DB  ------------>
                        // IF YES TO UNIQUE, CAN BE USED. OTHERWISE WE HAVE TO THROW A CUSTOM ERROR LETTING USER KNOW WHAT HAPPENED
                        var isUnique = !_context.BlogPosts.Any(blogpost => blogPost.Slug == slug);

                        if (isUnique)
                        {
                            blogPost.Slug = slug;
                        }

                        else
                        {
                            // THE SLUG CANNOT BE USED AND AN ERROR MUST BE SHOWN TO THE USER
                            ModelState.AddModelError("Title", "Incorrect TItle (duplicate SLUG)");
                            ModelState.AddModelError("", "Incorrect TItle (duplicate SLUG)");
                            ViewData["BlogId"] = new SelectList(_context.Blogs, "Id", "BlogId", blogPost.BlogId);
                            return View(blogPost);
                        }

                    }

                    blogPost.Updated = DateTime.UtcNow;
                    blogPost.Created = DateTime.SpecifyKind(blogPost.Created, DateTimeKind.Utc);

                    _context.Update(blogPost);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!BlogPostExists(blogPost.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }

            ViewData["BlogId"] = new SelectList(_context.Blogs, "Id", "BlogId", blogPost.BlogId);
            return View(blogPost);
        }


        // GET: BlogPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPosts
                .Include(b => b.Blog)
                .Include(c => c.Comments)
                .ThenInclude(c => c.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            _context.BlogPosts.Remove(blogPost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogPostExists(int id)
        {
            return _context.BlogPosts.Any(e => e.Id == id);
        }
    }
}
