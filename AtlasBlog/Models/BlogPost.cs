using AtlasBlog.Enums;
using System.ComponentModel.DataAnnotations;

namespace AtlasBlog.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        //[Required]
        [Display(Name = "Blog Identification")]
        public int BlogId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be a different number of characters long!", MinimumLength = 5)]
        public string Title { get; set; } = "";

        public string Slug { get; set; } = "";


        [Display(Name = "Mark for Deletion")]
        public bool IsDeleted { get; set; }


        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be a different number of characters long!", MinimumLength = 5)]
        public string Abstract { get; set; } = "";


        [Display(Name = "Post State")]
        public BlogPostState BlogPostState { get; set; }


        [Required]
        [StringLength(300, ErrorMessage = "The {0} must be a different number of characters long!", MinimumLength = 5)]
        public string Body { get; set; } = "";


        public DateTime Created { get; set; }


        [DataType(DataType.DateTime)]
        public DateTime? Updated { get; set; }

        


        // --------------- NAVIGATION PROPERTIES -----------------------------

        //public ICollection<BlogPost> BlogPosts { get; set; } = new HashSet<BlogPost>();
        public Blog? Blog { get; set; }

        //------------------------------ Tags -------------------------------

        // ---------------------------- COMMENTS --------------------------------
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
