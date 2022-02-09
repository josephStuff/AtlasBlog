﻿using AtlasBlog.Enums;
using System.ComponentModel.DataAnnotations;

namespace AtlasBlog.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        [Display(Name ="Blog Id")]
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
        public DateTime? Updated { get; set; }






        // --------------- NAVIGATION PROPERTIES -----------------------------
        public Blog? Blog { get; set; }

        //------------------------------ Tags -------------------------------

        // ---------------------------- COMMENTS --------------------------------

    }
}
