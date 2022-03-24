using AtlasBlog.Enums;
using System.ComponentModel.DataAnnotations;


namespace AtlasBlog.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class BlogPost
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// This is the BlogPost Foriegn Key
        /// </summary>
        [Required]
        public int BlogId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be a different number of characters long!", MinimumLength = 5)]
        public string Title { get; set; } = "";

        /// <summary>
        /// The Slug is derived from the title and must be unique 
        /// </summary>
        public string Slug { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Delete Soon")]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be a different number of characters long!", MinimumLength = 5)]
        public string Abstract { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Post State")]
        public BlogPostState BlogPostState { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(1000, ErrorMessage = "The {0} must be a different number of characters long!", MinimumLength = 5)]
        public string Body { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? Updated { get; set; }


        // --------------- NAVIGATION PROPERTIES -----------------------------
        /// <summary>
        /// 
        /// </summary>
        //public ICollection<BlogPost> BlogPosts { get; set; } = new HashSet<BlogPost>();
        public Blog? Blog { get; set; }

        //------------------------------ Tags -------------------------------

        // ---------------------------- COMMENTS --------------------------------
        /// <summary>
        /// 
        /// </summary>
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        /// <summary>
        /// 
        /// </summary>
        public ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();
        
    }
}
