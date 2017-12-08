using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.RawSql.Final.Helpers;
using MasteringEFCore.RawSql.Final.Models;

namespace MasteringEFCore.RawSql.Final.Data
{
    public class DbInitializer
    {
        public static void Initialize(BlogContext context)
        {
            context.Database.EnsureCreated();

            // Look for any person.
            if (context.People.Any())
            {
                return;   // DB has been seeded
            }

            context.People.Add(new Person
            {
                FirstName = "Prabhakaran",
                LastName = "Anbazhagan",
                NickName = "Prabhakar",
                Biography = "Author, Architect, Dad and Husband",
                PhoneNumber = "9876543210",
                CreatedAt = DateTime.Now
            });
            context.SaveChanges();

            context.Users.Add(new User
            {
                PersonId = 1,
                DisplayName = "Prabhakaran A",
                Email = "abc@xyz.com",
                PasswordHash = Cryptography.Instance.HashPassword("test@123"),
                Username = "prabhakar",
                CreatedAt = DateTime.Now
            });
            context.SaveChanges();

            context.Addresses.Add(new Address
            {
                UserId = 1,
                FlatHouseInfo = "ABC",
                StreetName = "DEF",
                City = "Chennai",
                Country = "India",
                State = "Tamil Nadu",
                CreatedAt = DateTime.Now,
                CreatedBy = 1
            });
            context.SaveChanges();

            var blogs = new[]
            {
                new Blog
                {
                    Url = "http://blogs.packtpub.com/dotnet",
                    Title = "Dot Net",
                    Subtitle = "Dot Net related blogs",
                    Description = "Dot Net related blogs",
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1
                },
                new Blog
                {
                    Url = "http://blogs.packtpub.com/dotnetcore",
                    Title = "Dot Net Core",
                    Subtitle = "Dot Net Core related blogs",
                    Description = "Dot Net Core related blogs",
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1
                }
            };
            foreach (var blog in blogs)
            {
                context.Blogs.Add(blog);
            }
            context.SaveChanges();

            var categories = new[]
            {
                new Category
                {
                    Name = "Dot Net",
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1
                },
                new Category
                {
                    Name = "Dot Net Core",
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1
                }
            };
            foreach (var category in categories)
            {
                context.Categories.Add(category);
            }
            context.SaveChanges();

            var posts = new[]
            {
                new Post
                {
                    Title = "Dotnet 4.7 Released",
                    BlogId = 1,
                    Content = "Dotnet 4.7 Released Contents",
                    PublishedDateTime = DateTime.Now,
                    AuthorId = 1,
                    CategoryId = 1,
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1
                },
                new Post
                {
                    Title = ".NET Core 1.1 Released",
                    BlogId = 2,
                    Content = ".NET Core 1.1 Released Contents",
                    PublishedDateTime = DateTime.Now,
                    AuthorId = 1,
                    CategoryId = 2,
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1
                },
                new Post
                {
                    Title = "EF Core 1.1 Released",
                    BlogId = 2,
                    Content = "EF Core 1.1 Released Contents",
                    PublishedDateTime = DateTime.Now,
                    AuthorId = 1,
                    CategoryId = 2,
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1
                }
            };
            foreach (var post in posts)
            {
                context.Posts.Add(post);
            }
            context.SaveChanges();

            var tags = new[]
            {
                new Tag
                {
                    Name = "Dot Net",
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1
                },
                new Tag
                {
                    Name = "Dot Net Core",
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1
                }
            };
            foreach (var tag in tags)
            {
                context.Tags.Add(tag);
            }
            context.SaveChanges();

            var tagPosts = new[]
            {
                new TagPost
                {
                    PostId = 1,
                    TagId = 1,
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1
                },
                new TagPost
                {
                    PostId = 2,
                    TagId = 2,
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1
                },
                new TagPost
                {
                    PostId = 3,
                    TagId = 2,
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1
                }
            };
            foreach (var tagPost in tagPosts)
            {
                context.TagPosts.Add(tagPost);
            }
            context.SaveChanges();
        }
    }
}
