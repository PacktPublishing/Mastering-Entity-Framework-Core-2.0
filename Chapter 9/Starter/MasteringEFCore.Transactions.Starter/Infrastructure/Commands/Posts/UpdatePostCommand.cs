using MasteringEFCore.Transactions.Starter.Core.Commands.Posts;
using MasteringEFCore.Transactions.Starter.Data;
using MasteringEFCore.Transactions.Starter.Helpers;
using MasteringEFCore.Transactions.Starter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Transactions.Starter.Infrastructure.Commands.Posts
{
    public class UpdatePostCommand : CommandBase, ICreatePostCommand<int>
    {
        public UpdatePostCommand(BlogContext context) : base(context)
        {
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public int BlogId { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public DateTime PublishedDateTime { get; set; }

        public int Handle()
        {
            Context.Update(new Post
            {
                Id = Id,
                Title = Title,
                Content = Content,
                Summary = Summary,
                BlogId = BlogId,
                AuthorId = AuthorId,
                CategoryId = CategoryId,
                PublishedDateTime = PublishedDateTime,
                ModifiedAt = DateTime.Now,
                ModifiedBy = AuthorId,
                Url = Title.Generate()
            });
            return Context.SaveChanges();
        }

        public async Task<int> HandleAsync()
        {
            Context.Update(new Post
            {
                Id = Id,
                Title = Title,
                Content = Content,
                Summary = Summary,
                BlogId = BlogId,
                AuthorId = AuthorId,
                CategoryId = CategoryId,
                PublishedDateTime = PublishedDateTime,
                CreatedAt = DateTime.Now,
                CreatedBy = AuthorId,
                Url = Title.Generate()
            });
            return await Context.SaveChangesAsync();
        }
    }
}
