using MasteringEFCore.Transactions.Starter.Data;
using MasteringEFCore.Transactions.Starter.Models;
using MasteringEFCore.Transactions.Starter.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Transactions.Starter.Handlers
{
    public class GetPostByCategoryQueryHandler : IPostQueryHandler<GetPostByCategoryQuery>
    {
        private readonly BlogContext _context;

        public GetPostByCategoryQueryHandler(BlogContext context)
        {
            this._context = context;
        }

        public IEnumerable<Post> Handle(GetPostByCategoryQuery query)
        {
            return query.IncludeData
                        ? _context.Posts
                            .Where(x => x.Category.Name.ToLower().Contains(query.Category.ToLower()))
                            .Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category).ToList()
                        : _context.Posts
                            .Where(x => x.Category.Name.ToLower().Contains(query.Category.ToLower()))
                            .ToList();
        }

        public async Task<IEnumerable<Post>> HandleAsync(GetPostByCategoryQuery query)
        {
            return query.IncludeData
                        ? await _context.Posts
                            .Where(x => x.Category.Name.ToLower().Contains(query.Category.ToLower()))
                            .Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category).ToListAsync()
                        : await _context.Posts
                            .Where(x => x.Category.Name.ToLower().Contains(query.Category.ToLower()))
                            .ToListAsync();
        }
    }
}
