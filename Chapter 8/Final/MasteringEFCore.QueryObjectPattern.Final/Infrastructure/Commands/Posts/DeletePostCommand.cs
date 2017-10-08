using MasteringEFCore.QueryObjectPattern.Final.Core.Commands.Posts;
using MasteringEFCore.QueryObjectPattern.Final.Data;
using MasteringEFCore.QueryObjectPattern.Final.Helpers;
using MasteringEFCore.QueryObjectPattern.Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.QueryObjectPattern.Final.Infrastructure.Commands.Posts
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
