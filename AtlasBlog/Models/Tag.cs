using System.ComponentModel.DataAnnotations;

namespace AtlasBlog.Models
{
    public class Tag
    {

        public int Id { get; set; }

        [Required]
        public string Text { get; set; } = "";
        public string Description { get; set; } = string.Empty;


        public virtual ICollection<BlogPost> BlogPosts { get; set; } = new HashSet<BlogPost>();
        
    }
}
