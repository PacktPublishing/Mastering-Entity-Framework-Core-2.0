using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.Performance.Starter.Models;
using Microsoft.EntityFrameworkCore;
using MasteringEFCore.Performance.Starter.ViewModels;
using System.Threading;

namespace MasteringEFCore.Performance.Starter.Data
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

        //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        //{
        //    ChangeTracker.DetectChanges();
        //    var now = DateTime.UtcNow;

        //    foreach (var item in ChangeTracker.Entries<Post>().Where(e => e.State == EntityState.Added))
        //    {
        //        item.Property("CreatedAt").CurrentValue = now;
        //        item.Property("ModifiedAt").CurrentValue = now;
        //    }

        //    foreach (var item in ChangeTracker.Entries<Post>().Where(e => e.State == EntityState.Modified))
        //    {
        //        item.Property("ModifiedAt").CurrentValue = now;
        //    }

        //    return await base.SaveChangesAsync(cancellationToken);
        //}

        //public override int SaveChanges()
        //{
        //    ChangeTracker.DetectChanges();
        //    var now = DateTime.UtcNow;

        //    foreach (var item in ChangeTracker.Entries<Post>().Where(e => e.State == EntityState.Added))
        //    {
        //        item.Property("CreatedAt").CurrentValue = now;
        //        item.Property("ModifiedAt").CurrentValue = now;
        //    }

        //    foreach (var item in ChangeTracker.Entries<Post>().Where(e => e.State == EntityState.Modified))
        //    {
        //        item.Property("ModifiedAt").CurrentValue = now;
        //    }


        //    return base.SaveChanges();
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .ToTable("Blog");
                //.Property(x => x.ModifiedAt)
                //.IsConcurrencyToken();
            //modelBuilder.Entity<Blog>()
            //    .Property(p => p.Timestamp)
            //    .ValueGeneratedOnAddOrUpdate()
            //    .IsConcurrencyToken();
            modelBuilder.Entity<Post>()
                .ToTable("Post")
                .HasOne(x=>x.Author)
                .WithMany(x=>x.Posts)
                .HasForeignKey(x=>x.AuthorId)
                .IsRequired();
            //modelBuilder.Entity<Post>()
            //    .ToTable("Post")
            //    .Property(x => x.ModifiedAt)
            //    .ValueGeneratedOnAddOrUpdate()
            //    .HasDefaultValueSql("GETUTCDATE()")
            //    .IsConcurrencyToken();
            //modelBuilder.Entity<Post>()
            //    .Property(p => p.Timestamp)
            //    .ValueGeneratedOnAddOrUpdate()
            //    .IsConcurrencyToken();
            modelBuilder.Entity<User>()
                .ToTable("User")
                .HasOne(x=>x.Address)
                .WithOne(x=>x.User)
                .HasForeignKey<Address>(x=>x.UserId);
            //modelBuilder.Entity<User>()
            //    .ToTable("User")
            //    .Property(x => x.ModifiedAt)
            //    .IsConcurrencyToken();
            //modelBuilder.Entity<User>()
            //    .Property(p => p.Timestamp)
            //    .ValueGeneratedOnAddOrUpdate()
            //    .IsConcurrencyToken();
            modelBuilder.Entity<Address>().ToTable("Address");
            //modelBuilder.Entity<Address>()
            //    .ToTable("Address")
            //    .Property(x => x.ModifiedAt)
            //    .IsConcurrencyToken();
            //modelBuilder.Entity<Address>()
            //    .Property(p => p.Timestamp)
            //    .ValueGeneratedOnAddOrUpdate()
            //    .IsConcurrencyToken();
            modelBuilder.Entity<Tag>().ToTable("Tag");
            //modelBuilder.Entity<Tag>()
            //    .ToTable("Tag")
            //    .Property(x => x.ModifiedAt)
            //    .IsConcurrencyToken();
            //modelBuilder.Entity<Tag>()
            //    .Property(p => p.Timestamp)
            //    .ValueGeneratedOnAddOrUpdate()
            //    .IsConcurrencyToken();
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
            //modelBuilder.Entity<TagPost>()
            //    .ToTable("TagPost")
            //    .Property(x => x.ModifiedAt)
            //    .IsConcurrencyToken();
            //modelBuilder.Entity<TagPost>()
            //    .Property(p => p.Timestamp)
            //    .ValueGeneratedOnAddOrUpdate()
            //    .IsConcurrencyToken();
            modelBuilder.Entity<Category>()
                .ToTable("Category")
                // HasOptional() was not part of EF Core, HasOne() accepts NULL values so we use it as work around
                .HasOne(x => x.ParentCategory)
                .WithMany(x => x.Subcategories)
                .HasForeignKey(x => x.ParentCategoryId)
                // WillCascadeOnDelete() not available in EF Core, so we use IsRequired(false) as work around
                .IsRequired(false);
            //modelBuilder.Entity<Category>()
            //    .ToTable("Category")
            //    .Property(x => x.ModifiedAt)
            //    .IsConcurrencyToken();
            //modelBuilder.Entity<Category>()
            //    .Property(p => p.Timestamp)
            //    .ValueGeneratedOnAddOrUpdate()
            //    .IsConcurrencyToken();
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
            //modelBuilder.Entity<Comment>()
            //    .ToTable("Comment")
            //    .Property(x => x.ModifiedAt)
            //    .IsConcurrencyToken();
            //modelBuilder.Entity<Comment>()
            //    .Property(p => p.Timestamp)
            //    .ValueGeneratedOnAddOrUpdate()
            //    .IsConcurrencyToken();
            modelBuilder.Entity<Person>()
                .ToTable("Person")
                .HasOne(x=>x.User)
                .WithOne(x=>x.Person)
                .HasForeignKey<User>(x=>x.PersonId);
            //modelBuilder.Entity<Person>()
            //    .ToTable("Person")
            //    .Property(x => x.ModifiedAt)
            //    .IsConcurrencyToken();
            //modelBuilder.Entity<Person>()
            //    .Property(p => p.Timestamp)
            //    .ValueGeneratedOnAddOrUpdate()
            //    .IsConcurrencyToken();
        }
    }
}
