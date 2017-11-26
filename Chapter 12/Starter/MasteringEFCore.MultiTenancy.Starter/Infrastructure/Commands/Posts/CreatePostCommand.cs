using MasteringEFCore.MultiTenancy.Starter.Core.Commands.Posts;
using MasteringEFCore.MultiTenancy.Starter.Helpers;
using MasteringEFCore.MultiTenancy.Starter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.MultiTenancy.Starter.Data;
using System.Runtime.ExceptionServices;
using Microsoft.EntityFrameworkCore.Storage;
using MasteringEFCore.MultiTenancy.Starter.Infrastructure.Commands.Files;

namespace MasteringEFCore.MultiTenancy.Starter.Infrastructure.Commands.Posts
{
    public class CreatePostCommand : CommandBase, ICreatePostCommand<int>
    {
        public CreatePostCommand(BlogContext context) : base(context)
        {
        }

        public string Title { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public int BlogId { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public ICollection<int> TagIds { get; set; }
        public DateTime PublishedDateTime { get; set; }
        public Guid FileId { get; set; }

        public int Handle()
        {
            int returnValue = 0;
            try
            {
                Post post = new Post
                {
                    Title = Title,
                    Content = Content,
                    Summary = Summary,
                    BlogId = BlogId,
                    AuthorId = AuthorId,
                    CategoryId = CategoryId,
                    PublishedDateTime = PublishedDateTime,
                    CreatedAt = DateTime.Now,
                    CreatedBy = AuthorId,
                    ModifiedAt = DateTime.Now,
                    ModifiedBy = AuthorId,
                    Url = Title.Generate(),
                    FileId = FileId
                };

                Context.Add(post);
                returnValue = Context.SaveChanges();

                Context.ChangeTracker.AutoDetectChangesEnabled = false;
                foreach (int tagId in TagIds)
                {
                    Context.Add(new TagPost
                    {
                        TagId = tagId,
                        Post = post
                    });
                }
                Context.ChangeTracker.AutoDetectChangesEnabled = true;
                returnValue = Context.SaveChanges();
            }
            catch (Exception exception)
            {
                ExceptionDispatchInfo.Capture(exception.InnerException).Throw();
            }
            return returnValue;
        }

        public async Task<int> HandleAsync()
        {
            int returnValue = 0;
            try
            {
                Post post = new Post
                {
                    Title = Title,
                    Content = Content,
                    Summary = Summary,
                    BlogId = BlogId,
                    AuthorId = AuthorId,
                    CategoryId = CategoryId,
                    PublishedDateTime = PublishedDateTime,
                    CreatedAt = DateTime.Now,
                    CreatedBy = AuthorId,
                    ModifiedAt = DateTime.Now,
                    ModifiedBy = AuthorId,
                    Url = Title.Generate(),
                    FileId = FileId
                };

                Context.Add(post);
                returnValue = await Context.SaveChangesAsync();

                Context.ChangeTracker.AutoDetectChangesEnabled = false;
                foreach (int tagId in TagIds)
                {
                    Context.Add(new TagPost
                    {
                        TagId = tagId,
                        Post = post
                    });
                }

                Context.ChangeTracker.AutoDetectChangesEnabled = true;
                returnValue = await Context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                ExceptionDispatchInfo.Capture(exception.InnerException).Throw();
            }
            return returnValue;
        }
    }
}
