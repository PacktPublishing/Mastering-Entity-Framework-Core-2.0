using MasteringEFCore.Transactions.Starter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.Transactions.Starter.Models;
using MasteringEFCore.Transactions.Starter.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MasteringEFCore.Transactions.Starter.Handlers
{
    public class GetAllPostsQueryHandler : IPostQueryHandler<GetAllPostsQuery>
    {
        private readonly BlogContext _context;

        public GetAllPostsQueryHandler(BlogContext context)
        {
            this._context = context;
        }

        public IEnumerable<Post> Handle(GetAllPostsQuery query)
        {
            return query.IncludeData 
                        ? _context.Posts.Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category).ToList()
                        : _context.Posts.ToList();
        }

        public async Task<IEnumerable<Post>> HandleAsync(GetAllPostsQuery query)
        {
            return query.IncludeData 
                        ? await _context.Posts.Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category).ToListAsync()
                        : await _context.Posts.ToListAsync();
        }
    }
}
