#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AtlasBlog.Data;
using AtlasBlog.Models;
using AtlasBlog.Enums;

namespace AtlasBlog.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BlogPostsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogPost>>> GetTopXPosts(int num)
        {
            //How to return top latest production ready posts that aren't deleted
            //  The latest Posts ----------------

            var posts = await _context.BlogPosts.Where(b => !b.IsDeleted &&
                                                    b.BlogPostState == BlogPostState.DefinitelyReady)
                                 .OrderByDescending(b => b.Created)
                                 .Take(num)
                                 .ToListAsync();

            return posts;
        }

    }
}
