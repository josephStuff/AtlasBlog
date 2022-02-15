﻿using AtlasBlog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AtlasBlog.Data
{
    public class ApplicationDbContext : IdentityDbContext<BlogUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Blog> Blogs { get; set; } = default!;
        public DbSet<BlogPost> BlogPosts { get; set; } = default!;
        public DbSet<AtlasBlog.Models.Comment> Comment { get; set; } = default!;


    }
}