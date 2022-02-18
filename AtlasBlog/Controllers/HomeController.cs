﻿using AtlasBlog.Data;
using AtlasBlog.Models;
using AtlasBlog.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace AtlasBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IImageService _imageService;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IImageService imageService)
        {
            _logger = logger;
            _context = context;
            _imageService = imageService;
        }

        public IActionResult Index()
        {
            var blogs = _context.Blogs.ToList();
            return View(blogs);
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BlogName,Description,Created,Updated")] Blog blog, IFormFile imageFile)
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

                    blog.ImageData = await _imageService.ConvertFileToByteArrayAsync(imageFile);
                    blog.ImageExt = imageFile.ContentType;

                    _context.Update(blog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (blog.Id == null)
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}