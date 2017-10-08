using MasteringEFCore.QueryObjectPattern.Final.Data;
using MasteringEFCore.QueryObjectPattern.Final.Models;
using MasteringEFCore.QueryObjectPattern.Final.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.QueryObjectPattern.Final.Handlers
{
    public class GetPostByHighestVisitorsQueryHandler : IPostQueryHandler<GetPostByHighestVisitorsQuery>
    {
        private readonly BlogContext _context;

        public GetPostByHighestVisitorsQueryHandler(BlogContext context)
        {
            this._context = context;
        }

        public IEnumerable<Post> Handle(GetPostByHighestVisitorsQuery query)
        {
            return query.IncludeData
                        ? _context.Posts
                            .OrderByDescending(x=>x.VisitorCount)
                            .Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category).ToList()
                        : _context.Posts
                            .OrderByDescending(x => x.VisitorCount)
                            .ToList();
        }

        public async Task<IEnumerable<Post>> HandleAsync(GetPostByHighestVisitorsQuery query)
        {
            return query.IncludeData
                        ? await _context.Posts
                            .OrderByDescending(x => x.VisitorCount)
                            .Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category).ToListAsync()
                        : await _context.Posts
                            .OrderByDescending(x => x.VisitorCount)
                            .ToListAsync();
        }
    }
}
