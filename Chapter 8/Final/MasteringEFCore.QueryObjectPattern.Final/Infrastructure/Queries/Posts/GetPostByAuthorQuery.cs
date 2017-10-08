using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.QueryObjectPattern.Final.Data;
using MasteringEFCore.QueryObjectPattern.Final.Core.Queries.Posts;
using MasteringEFCore.QueryObjectPattern.Final.Core.Queries;
using MasteringEFCore.QueryObjectPattern.Final.Models;
using Microsoft.EntityFrameworkCore;

namespace MasteringEFCore.QueryObjectPattern.Final.Infrastructure.Queries.Posts
{
    public class GetPostByAuthorQuery : QueryBase, IGetPostByAuthorQuery<GetPostByAuthorQuery>
    {
        public GetPostByAuthorQuery(BlogContext context) : base(context)
        {
        }

        public string Author { get; set; }
        public bool IncludeData { get; set; }

        public IEnumerable<Post> Handle()
        {
            return IncludeData
                        ? Context.Posts
                            .Where(x => x.Category.Name.ToLower().Contains(Author.ToLower()))
                            .Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category)
                            .ToList()
                        : Context.Posts
                            .Where(x => x.Category.Name.ToLower().Contains(Author.ToLower()))
                            .ToList();
        }

        public async Task<IEnumerable<Post>> HandleAsync()
        {
            return IncludeData
                        ? await Context.Posts
                            .Where(x => x.Category.Name.ToLower().Contains(Author.ToLower()))
                            .Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category)
                            .ToListAsync()
                        : await Context.Posts
                            .Where(x => x.Category.Name.ToLower().Contains(Author.ToLower()))
                            .ToListAsync();
        }
    }
}
