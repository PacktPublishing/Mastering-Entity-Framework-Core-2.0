using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.Transactions.Starter.Data;
using MasteringEFCore.Transactions.Starter.Core.Queries.Posts;
using MasteringEFCore.Transactions.Starter.Core.Queries;
using MasteringEFCore.Transactions.Starter.Models;
using Microsoft.EntityFrameworkCore;

namespace MasteringEFCore.Transactions.Starter.Infrastructure.Queries.Posts
{
    public class GetAllPostsQuery : QueryBase, IGetAllPostsQuery<GetAllPostsQuery>
    {
        public GetAllPostsQuery(BlogContext context) : base(context)
        {
        }

        public bool IncludeData { get; set; }

        public IEnumerable<Post> Handle()
        {
            return IncludeData
                        ? Context.Posts
                        .Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category)
                        .ToList()
                        : Context.Posts
                        .ToList();
        }

        public async Task<IEnumerable<Post>> HandleAsync()
        {
            return IncludeData
                        ? await Context.Posts
                        .Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category)
                        .ToListAsync()
                        : await Context.Posts
                        .ToListAsync();
        }
    }
}
