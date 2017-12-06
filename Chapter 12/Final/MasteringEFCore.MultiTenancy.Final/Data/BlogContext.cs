using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.MultiTenancy.Final.Models;
using Microsoft.EntityFrameworkCore;
using MasteringEFCore.MultiTenancy.Final.ViewModels;
using System.Threading;
using MasteringEFCore.MultiTenancy.Final.Exceptions;

namespace MasteringEFCore.MultiTenancy.Final.Data
{
    public class BlogContext: DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {
            
        }

        public Guid TenantId { get; set; }

        public DbSet<Tenant> Tenants { get; set; }
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
                .HasQueryFilter(item => item.TenantId.Equals(TenantId));
            //.Property(x => x.ModifiedAt)
            //.IsConcurrencyToken();
            //modelBuilder.Entity<Blog>()
            //    .Property(p => p.Timestamp)
            //    .ValueGeneratedOnAddOrUpdate()
            //    .IsConcurrencyToken();
            modelBuilder.Entity<Post>()
                .ToTable("Post")
                .HasQueryFilter(item => item.TenantId.Equals(TenantId));
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
            modelBuilder.Entity<Tenant>()
                .HasOne(x => x.Person)
                .WithOne(x => x.Tenant)
                .HasForeignKey<Person>(x => x.TenantId);
        }

        public override int SaveChanges()
        {
            ValidateMultiTenantPersistence();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ValidateMultiTenantPersistence();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            ValidateMultiTenantPersistence();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            ValidateMultiTenantPersistence();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void ValidateMultiTenantPersistence()
        {
            var tenantIds = ChangeTracker.Entries()
                            .Where(item => item.Entity is EntityBase)
                            .Select(item => ((EntityBase)item.Entity).TenantId)
                            .Distinct();

            if (!tenantIds.Any()) return;
            if (tenantIds.Count() > 1 || 
                !(tenantIds.Count().Equals(1) && tenantIds.First().Equals(TenantId)))
            {
                throw new MultiTenantException("Invalid tenant id(s) found: " + string.Join(", ", tenantIds));
            }
         }
    }
}
