using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.QueryObjectPattern.Final.Models;
using MasteringEFCore.QueryObjectPattern.Final.ViewModels;
using MasteringEFCore.QueryObjectPattern.Final.Data;
using Microsoft.EntityFrameworkCore;

namespace MasteringEFCore.QueryObjectPattern.Final.Handlers
{
    public class PostDetailQueryHandler : IPostDetailQueryHandler
    {
        private readonly BlogContext _context;

        public PostDetailQueryHandler(BlogContext context)
        {
            _context = context;
        }

        public async Task<Post> Handle(PostDetailQuery query)
        {
            return await _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Blog)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(x => x.Id == query.Id);
        }
    }
}
