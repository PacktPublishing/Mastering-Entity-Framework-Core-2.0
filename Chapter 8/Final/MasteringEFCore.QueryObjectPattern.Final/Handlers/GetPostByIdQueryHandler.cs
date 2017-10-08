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
    public class GetPostByIdQueryHandler : IPostQuerySingleHandler<GetPostByIdQuery>
    {
        private readonly BlogContext _context;

        public GetPostByIdQueryHandler(BlogContext context)
        {
            this._context = context;
        }

        public Post Handle(GetPostByIdQuery query)
        {
            return query.IncludeData
                        ? _context.Posts.Include(p => p.Author)
                            .Include(p => p.Blog)
                            .Include(p => p.Category).SingleOrDefault(x => x.Id.Equals(query.Id))
                        : _context.Posts
                            .SingleOrDefault(x => x.Id.Equals(query.Id));
        }

        public async Task<Post> HandleAsync(GetPostByIdQuery query)
        {
            return query.IncludeData
                        ? await _context.Posts.Include(p => p.Author)
                            .Include(p => p.Blog)
                            .Include(p => p.Category).SingleOrDefaultAsync(x => x.Id.Equals(query.Id))
                        : await _context.Posts
                            .SingleOrDefaultAsync(x => x.Id.Equals(query.Id));
        }
    }
}
