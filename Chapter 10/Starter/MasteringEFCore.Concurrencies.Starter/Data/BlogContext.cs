using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.Concurrencies.Starter.Models;
using Microsoft.EntityFrameworkCore;
using MasteringEFCore.Concurrencies.Starter.ViewModels;

namespace MasteringEFCore.Concurrencies.Starter.Data
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
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .ToTable("Blog")
                .Property(x=>x.ModifiedAt)
                .IsConcurrencyToken();
            modelBuilder.Entity<Post>()
                .ToTable("Post")
                .HasOne(x=>x.Author)
                .WithMany(x=>x.Posts)
                .HasForeignKey(x=>x.AuthorId)
                .IsRequired();
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
            modelBuilder.Entity<Category>()
                .ToTable("Category")
                // HasOptional() was not part of EF Core, HasOne() accepts NULL values so we use it as work around
                .HasOne(x => x.ParentCategory)
                .WithMany(x => x.Subcategories)
                .HasForeignKey(x => x.ParentCategoryId)
                // WillCascadeOnDelete() not available in EF Core, so we use IsRequired(false) as work around
                .IsRequired(false);
            modelBuilder.Entity<Comment>()
                .ToTable("Comment")
                .HasOne(x=>x.Person)
                .WithMany(x=>x.Comments)
                .HasForeignKey(x=>x.PersonId)
                .IsRequired(false);
            modelBuilder.Entity<Comment>()
                .ToTable("Comment")
                .HasOne(x => x.User)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.UserId)
                .IsRequired(false);
            modelBuilder.Entity<Person>()
                .ToTable("Person")
                .HasOne(x=>x.User)
                .WithOne(x=>x.Person)
                .HasForeignKey<User>(x=>x.PersonId);
        }
    }
}
