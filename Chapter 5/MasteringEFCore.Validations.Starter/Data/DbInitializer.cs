using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.Validations.Starter.Models;

namespace MasteringEFCore.Validations.Starter.Data
{
    public class DbInitializer
    {
        public static void Initialize(BlogContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            context.Users.Add(new User { Name = "Prabhakaran A" });
            context.SaveChanges();

            context.Addresses.Add(new Address { UserId = 1, FlatHouseInfo = "ABC", City = "Chennai", Country = "India" });
            context.SaveChanges();


            var blogs = new Blog[]
            {
            new Blog{Url= "http://blogs.packtpub.com/dotnet", AuthorId = 1},
            new Blog{Url= "http://blogs.packtpub.com/dotnetcore", AuthorId = 1}
            };
            foreach (var blog in blogs)
            {
                context.Blogs.Add(blog);
            }
            context.SaveChanges();

            var posts = new Post[]
            {
            new Post{Title="Dotnet 4.7 Released",BlogId= 1, Content = "Dotnet 4.7 Released Contents", PublishedDateTime = DateTime.Now},
            new Post{Title=".NET Core 1.1 Released",BlogId= 2, Content = ".NET Core 1.1 Released Contents", PublishedDateTime = DateTime.Now},
            new Post{Title="EF Core 1.1 Released",BlogId= 2, Content = "EF Core 1.1 Released Contents", PublishedDateTime = DateTime.Now}
            };
            foreach (var post in posts)
            {
                context.Posts.Add(post);
            }
            context.SaveChanges();
        }
    }
}
