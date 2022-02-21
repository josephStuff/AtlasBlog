using System.ComponentModel.DataAnnotations;

namespace AtlasBlog.Models
{
    public class Blog
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Blog Name")]
        [StringLength(100, ErrorMessage = "The {0} must be a different number of characters long!", MinimumLength = 2)]
        public string BlogName { get; set; } = "";

        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be a different number of characters long!", MinimumLength = 2)]
        public string Description { get; set;} = "";

        [DataType(DataType.Date)]
        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }


        [Required]
        [Display(Name = "Actual Code")]
        public string? ResearchTopic { get; set; }

        [Required]
        public string Title { get; set; } = "";

        //  THIS MODEL SHOULD HAVE A LIST OF POSTS AS CHILDREN --------------------------->
        public ICollection<BlogPost> BlogPosts { get; set; } = new HashSet<BlogPost>();
    }
}
