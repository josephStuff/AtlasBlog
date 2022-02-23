using AtlasBlog.Models;

namespace AtlasBlog.ViewModels
{
    public class BorrowedViewModel
    {
        // ----  THIS WILL BE FOR A PAGED LIST ---  IF PAGINATION WILL BE USED -----<
        public List<Blog> Blogs { get; set; } = default!;
        public List<BlogPost> BlogPosts { get; set; } = default!;
        

    }
}
