using MasteringEFCore.Transactions.Starter.Core.Queries.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.Transactions.Starter.Data;
using MasteringEFCore.Transactions.Starter.Core.Queries;
using MasteringEFCore.Transactions.Starter.Models;
using Microsoft.EntityFrameworkCore;

namespace MasteringEFCore.Transactions.Starter.Infrastructure.Queries.Posts
{
    public class GetPostByIdQuery : QueryBase, IGetPostByIdQuery<GetPostByIdQuery>
    {
        public GetPostByIdQuery(BlogContext context) : base(context)
        {
        }

        public int? Id { get; set; }
        public bool IncludeData { get; set; }

        public Post Handle()
        {
            return IncludeData
                           ? Context.Posts.Include(p => p.Author)
                               .Include(p => p.Blog)
                               .Include(p => p.Category).SingleOrDefault(x => x.Id.Equals(Id))
                           : Context.Posts
                               .SingleOrDefault(x => x.Id.Equals(Id));
        }

        public async Task<Post> HandleAsync()
        {
            return IncludeData
                           ? await Context.Posts.Include(p => p.Author)
                               .Include(p => p.Blog).Include(p => p.Category)
                               .SingleOrDefaultAsync(x => x.Id.Equals(Id))
                           : await Context.Posts
                               .SingleOrDefaultAsync(x => x.Id.Equals(Id));
        }
    }
}
