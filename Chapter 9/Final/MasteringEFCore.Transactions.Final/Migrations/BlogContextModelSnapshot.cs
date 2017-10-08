using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MasteringEFCore.Transactions.Final.Data;

namespace MasteringEFCore.Transactions.Final.Migrations
{
    [DbContext(typeof(BlogContext))]
    partial class BlogContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MasteringEFCore.Transactions.Final.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("Country")
                        .IsRequired();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("CreatedBy");

                    b.Property<string>("FlatHouseInfo")
                        .IsRequired();

                    b.Property<string>("LatitudeLongitude");

                    b.Property<string>("Locality");

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<int>("ModifiedBy");

                    b.Property<string>("State")
                        .IsRequired();

                    b.Property<string>("StreetName")
                        .IsRequired();

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Address");
                });

            modelBuilder.Entity("MasteringEFCore.Transactions.Final.Models.Blog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoryId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("CreatedBy");

                    b.Property<string>("Description");

                    b.Property<DateTime>("ModifiedAt")
                        .IsConcurrencyToken();

                    b.Property<int>("ModifiedBy");

                    b.Property<string>("Subtitle")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("Url")
                        .IsRequired();

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Blog");
                });

            modelBuilder.Entity("MasteringEFCore.Transactions.Final.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<int>("ModifiedBy");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("ParentCategoryId");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("MasteringEFCore.Transactions.Final.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CommentedAt");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<int>("ModifiedBy");

                    b.Property<int?>("PersonId");

                    b.Property<int>("PostId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("MasteringEFCore.Transactions.Final.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Biography");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("CreatedBy");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("ImageUrl");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<int>("ModifiedBy");

                    b.Property<string>("NickName");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("Url");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("MasteringEFCore.Transactions.Final.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AuthorId");

                    b.Property<int>("BlogId");

                    b.Property<int>("CategoryId");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("CreatedBy");

                    b.Property<Guid>("FileId");

                    b.Property<DateTime>("ModifiedAt")
                        .IsConcurrencyToken();

                    b.Property<int>("ModifiedBy");

                    b.Property<DateTime>("PublishedDateTime");

                    b.Property<string>("Summary");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("Url");

                    b.Property<long>("VisitorCount");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("BlogId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("MasteringEFCore.Transactions.Final.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<int>("ModifiedBy");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("MasteringEFCore.Transactions.Final.Models.TagPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<int>("ModifiedBy");

                    b.Property<int>("PostId");

                    b.Property<int>("TagId");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("TagId");

                    b.ToTable("TagPost");
                });

            modelBuilder.Entity("MasteringEFCore.Transactions.Final.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AddressId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("CreatedBy");

                    b.Property<string>("DisplayName")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<int>("ModifiedBy");

                    b.Property<string>("PasswordHash")
                        .IsRequired();

                    b.Property<int>("PersonId");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("MasteringEFCore.Transactions.Final.Models.Address", b =>
                {
                    b.HasOne("MasteringEFCore.Transactions.Final.Models.User", "User")
                        .WithOne("Address")
                        .HasForeignKey("MasteringEFCore.Transactions.Final.Models.Address", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MasteringEFCore.Transactions.Final.Models.Blog", b =>
                {
                    b.HasOne("MasteringEFCore.Transactions.Final.Models.Category")
                        .WithMany("Blogs")
                        .HasForeignKey("CategoryId");

                    b.HasOne("MasteringEFCore.Transactions.Final.Models.User")
                        .WithMany("Blogs")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("MasteringEFCore.Transactions.Final.Models.Category", b =>
                {
                    b.HasOne("MasteringEFCore.Transactions.Final.Models.Category", "ParentCategory")
                        .WithMany("Subcategories")
                        .HasForeignKey("ParentCategoryId");
                });

            modelBuilder.Entity("MasteringEFCore.Transactions.Final.Models.Comment", b =>
                {
                    b.HasOne("MasteringEFCore.Transactions.Final.Models.Person", "Person")
                        .WithMany("Comments")
                        .HasForeignKey("PersonId");

                    b.HasOne("MasteringEFCore.Transactions.Final.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MasteringEFCore.Transactions.Final.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("MasteringEFCore.Transactions.Final.Models.Post", b =>
                {
                    b.HasOne("MasteringEFCore.Transactions.Final.Models.User", "Author")
                        .WithMany("Posts")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MasteringEFCore.Transactions.Final.Models.Blog", "Blog")
                        .WithMany("Posts")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MasteringEFCore.Transactions.Final.Models.Category", "Category")
                        .WithMany("Posts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MasteringEFCore.Transactions.Final.Models.TagPost", b =>
                {
                    b.HasOne("MasteringEFCore.Transactions.Final.Models.Post", "Post")
                        .WithMany("TagPosts")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MasteringEFCore.Transactions.Final.Models.Tag", "Tag")
                        .WithMany("TagPosts")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MasteringEFCore.Transactions.Final.Models.User", b =>
                {
                    b.HasOne("MasteringEFCore.Transactions.Final.Models.Person", "Person")
                        .WithOne("User")
                        .HasForeignKey("MasteringEFCore.Transactions.Final.Models.User", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
