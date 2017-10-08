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
    public class DeletePostCommand : CommandBase, ICreatePostCommand<int>
    {
        public DeletePostCommand(BlogContext context) : base(context)
        {
        }

        public int Id { get; set; }

        public int Handle()
        {
            DeletePost();
            return Context.SaveChanges();
        }

        public async Task<int> HandleAsync()
        {
            DeletePost();
            return await Context.SaveChangesAsync();
        }

        private void DeletePost()
        {
            var post = Context.Posts.SingleOrDefault(m => m.Id == Id);
            Context.Posts.Remove(post);
        }
    }
}
