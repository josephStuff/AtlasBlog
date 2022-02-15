namespace AtlasBlog.Models
{
    public class Comment
    {

        public int Id { get; set; }
        public int BlogPostId { get; set; }

        // ---------------------  I NEED TO REFERENCE THE AUTHOR OF THIS COMMENT ---------------
        public string? AuthorId { get; set; }


        public string CommentBody { get; set; } = "";

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
            

        public bool IsDeleted { get; set; }


        // ------------- NAV PROPERTY THAT IS "LAZY LOADED" -------------------
        public virtual BlogPost? BlogPost { get; set; }
        public virtual BlogUser? Author { get; set; }

        
    }
}
