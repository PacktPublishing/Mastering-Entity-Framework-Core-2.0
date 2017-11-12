using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.Performance.Final.Data;
using MasteringEFCore.Performance.Final.Core.Queries.Posts;
using MasteringEFCore.Performance.Final.Core.Queries;
using MasteringEFCore.Performance.Final.Models;
using Microsoft.EntityFrameworkCore;

namespace MasteringEFCore.Performance.Final.Infrastructure.Queries.Posts
{
    public class GetPostByHighestVisitorsQuery : QueryBase, IGetPostByHighestVisitorsQuery<GetPostByHighestVisitorsQuery>
    {
        public GetPostByHighestVisitorsQuery(BlogContext context) : base(context)
        {
        }

        public bool IncludeData { get; set; }

        public IEnumerable<Post> Handle()
        {
            return IncludeData
                        ? Context.Posts
                            .OrderByDescending(x => x.VisitorCount)
                            .Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category)
                            .ToList()
                        : Context.Posts
                            .OrderByDescending(x => x.VisitorCount)
                            .ToList();
        }

        public async Task<IEnumerable<Post>> HandleAsync()
        {
            return IncludeData
                        ? await Context.Posts
                            .OrderByDescending(x => x.VisitorCount)
                            .Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category)
                            .ToListAsync()
                        : await Context.Posts
                            .OrderByDescending(x => x.VisitorCount)
                            .ToListAsync();
        }
    }
}
