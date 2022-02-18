﻿using System.ComponentModel.DataAnnotations;

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
        [StringLength(300, ErrorMessage = "The {0} must be a different number of characters long!", MinimumLength = 2)]
        public string Description { get; set;} = "";

        [DataType(DataType.Date)]
        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        [Display(Name = "Input Code")]
        public string? ResearchTopic { get; set; }

        //public byte[] ImageData { get; set; } = Array.Empty<byte>();
        //public string ImageExt { get; set; } = "";

        public string Title { get; set; } = "";

        //  THIS MODEL SHOULD HAVE A LIST OF POSTS AS CHILDREN --------------------------->
        public ICollection<BlogPost> BlogPosts { get; set; } = new HashSet<BlogPost>();
    }
}
