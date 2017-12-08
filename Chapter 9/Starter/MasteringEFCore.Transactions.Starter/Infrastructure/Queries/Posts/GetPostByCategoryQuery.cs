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
    public class GetPostByCategoryQuery : QueryBase, IGetPostByCategoryQuery<GetPostByCategoryQuery>
    {
        public GetPostByCategoryQuery(BlogContext context) : base(context)
        {
        }

        public string Category { get; set; }
        public bool IncludeData { get; set; }

        public IEnumerable<Post> Handle()
        {
            return IncludeData
                        ? Context.Posts
                            .Where(x => x.Category.Name.ToLower().Contains(Category.ToLower()))
                            .Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category)
                            .ToList()
                        : Context.Posts
                            .Where(x => x.Category.Name.ToLower().Contains(Category.ToLower()))
                            .ToList();
        }

        public async Task<IEnumerable<Post>> HandleAsync()
        {
            return IncludeData
                        ? await Context.Posts
                            .Where(x => x.Category.Name.ToLower().Contains(Category.ToLower()))
                            .Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category)
                            .ToListAsync()
                        : await Context.Posts
                            .Where(x => x.Category.Name.ToLower().Contains(Category.ToLower()))
                            .ToListAsync();
        }
    }
}
