using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.Validations.Starter.Models;
using Microsoft.EntityFrameworkCore;

namespace MasteringEFCore.Validations.Starter.Data
{
    public class BlogContext: DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {
            
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagPost> TagPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>().ToTable("Blog");
            modelBuilder.Entity<Post>().ToTable("Post");
            modelBuilder.Entity<User>()
                .ToTable("User")
                .HasOne(x=>x.Address)
                .WithOne(x=>x.User)
                .HasForeignKey<Address>(x=>x.UserId);
            modelBuilder.Entity<Address>().ToTable("Address");
            modelBuilder.Entity<Tag>().ToTable("Tag");
            modelBuilder.Entity<TagPost>()
                .ToTable("TagPost")
                .HasOne(x => x.Tag)
                .WithMany(x => x.TagPosts)
                .HasForeignKey(x => x.TagId);

            modelBuilder.Entity<TagPost>()
                .ToTable("TagPost")
                .HasOne(x => x.Post)
                .WithMany(x => x.TagPosts)
                .HasForeignKey(x => x.PostId);
        }
    }
}
