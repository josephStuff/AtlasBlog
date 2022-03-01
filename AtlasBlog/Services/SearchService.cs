using AtlasBlog.Data;
using AtlasBlog.Models;


namespace AtlasBlog.Services
{
    public class SearchService
    {

        private readonly ApplicationDbContext _dbContext;

        public SearchService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<BlogPost> TermSearch(string searchTerm)
        {

            var resultSet = _dbContext.BlogPosts
                                        .Where(b => b.BlogPostState == Enums.BlogPostState
                                         .DefinitelyReady && !b.IsDeleted).AsQueryable();

            // ---------  IF SEARCH TERM IS SUPPLIED   ---   LOOK FOR IT INSIDE THE resultSet -------<

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm?.ToLower();

                resultSet = resultSet.Where(bp =>
                                            bp.Title.ToLower().Contains(searchTerm) ||
                                            bp.Abstract.ToLower().Contains(searchTerm) ||
                                            bp.Body.ToLower().Contains(searchTerm) ||
                                            bp.Comments.Any(c =>
                                                            c.CommentBody.ToLower().Contains(searchTerm) ||
                                                            c.ModeratedBody!.ToLower().Contains(searchTerm) ||
                                                            c.Author!.FirstName.ToLower().Contains(searchTerm) ||
                                                            c.Author.LastName.ToLower().Contains(searchTerm) ||
                                                            c.Author.Email.ToLower().Contains(searchTerm)));

            }

            return resultSet.OrderByDescending(r => r.Comments);

        }

    }
}
