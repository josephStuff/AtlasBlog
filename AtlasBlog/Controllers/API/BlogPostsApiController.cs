#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AtlasBlog.Data;
using AtlasBlog.Models;
using AtlasBlog.Enums;

namespace AtlasBlog.Controllers.API
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public BlogPostsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns the specified number of latest Posts
        /// </summary>
        /// <param name="num">inter count of records</param>
        /// <returns>
        /// Returns a list of Blog Posts
        /// </returns>

        [HttpGet("GetTopXPosts/{num:int}")]
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
