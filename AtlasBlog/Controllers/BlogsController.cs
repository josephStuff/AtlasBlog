﻿#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AtlasBlog.Data;
using AtlasBlog.Models;
using AtlasBlog.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using AtlasBlog.Services;
using AtlasBlog.ViewModels;

namespace AtlasBlog.Controllers
{
    public class BlogsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageService _imageService;


        public BlogsController(ApplicationDbContext context, IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        // GET: Blogs
        public async Task<IActionResult> Index()
        {

            //var blogPosts = new List<BlogPost>();
            //var model = await _context.Blogs.OrderByDescending(b => b.Created).Max(b => b.Created);

            //var model = await _context.Blogs.OrderByDescending(b => b.Created).ToListAsync();
            var model = await _context.Blogs.Include(b => b.BlogPosts).ToListAsync();


            return View(model);
            //await _context.Blogs.ToListAsync()
        }

        // GET: Blogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                                           .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }


        // GET: Blogs/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }


        // POST: Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogName,Description,Title,ResearchTopic")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                if (blog is not null)
                {
                    //blog.ResearchTopic = await _imageService.ConvertFileToByteArrayAsync(blog);
                    //blog.ImageExt = imageFile.ContentType;
                }
                //else
                //{
                //    return)
                //}

                // ------- SPECIFY THE DATETIME KIND FOR THE INCOMING CREATED DATE ----------------------------->
                blog.Created = DateTime.UtcNow;

                _context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(blog);
        }

        // GET: Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var blog = await _context.Blogs.Include(b => b.BlogPosts).FindAsync(id);
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)                                        //.FindAsync(id);

            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BlogName,Description,Title,ResearchTopic,Created,Updated")] Blog blog)
        {
            if (id != blog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    blog.Updated = DateTime.UtcNow;
                    blog.Created = DateTime.SpecifyKind(blog.Created, DateTimeKind.Utc);

                    //blog.ResearchTopic = await _imageService.ConvertFileToByteArrayAsync(blog);
                    //blog.ImageExt = imageFile.ContentType;

                    _context.Update(blog);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.Id == id);
        }
    }
}
