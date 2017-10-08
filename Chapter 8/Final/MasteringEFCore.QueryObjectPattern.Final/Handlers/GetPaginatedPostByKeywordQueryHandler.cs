using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.QueryObjectPattern.Final.Models;
using MasteringEFCore.QueryObjectPattern.Final.ViewModels;
using MasteringEFCore.QueryObjectPattern.Final.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace MasteringEFCore.QueryObjectPattern.Final.Handlers
{
    public class GetPaginatedPostByKeywordQueryHandler : IPostQueryHandler<GetPaginatedPostByKeywordQuery>
    {
        private readonly BlogContext _context;

        public GetPaginatedPostByKeywordQueryHandler(BlogContext context)
        {
            this._context = context;
        }

        public IEnumerable<Post> Handle(GetPaginatedPostByKeywordQuery query)
        {
            var keyword = query.Keyword;
            Expression<Func<Post, bool>> wherePredicate = x => x.Title.ToLower().Contains(keyword.ToLower())
                                                                || x.Blog.Title.ToLower().Contains(keyword.ToLower())
                    || x.Blog.Subtitle.ToLower().Contains(keyword.ToLower())
                    || x.Category.Name.ToLower().Contains(keyword.ToLower())
                    || x.Content.ToLower().Contains(keyword.ToLower())
                    || x.Summary.ToLower().Contains(keyword.ToLower())
                    || x.Author.Username.ToLower().Contains(keyword.ToLower())
                    || x.Url.ToLower().Contains(keyword.ToLower());
            return query.IncludeData
                    ? _context.Posts.Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category)
                        .Where(wherePredicate).Skip(query.PageNumber - 1).Take(query.PageCount).ToList()
                    : _context.Posts.Where(wherePredicate).Skip(query.PageNumber - 1).Take(query.PageCount).ToList();
        }

        public async Task<IEnumerable<Post>> HandleAsync(GetPaginatedPostByKeywordQuery query)
        {
            var keyword = query.Keyword;
            Expression<Func<Post, bool>> wherePredicate = x => x.Title.ToLower().Contains(keyword.ToLower())
                                                                || x.Blog.Title.ToLower().Contains(keyword.ToLower())
                    || x.Blog.Subtitle.ToLower().Contains(keyword.ToLower())
                    || x.Category.Name.ToLower().Contains(keyword.ToLower())
                    || x.Content.ToLower().Contains(keyword.ToLower())
                    || x.Summary.ToLower().Contains(keyword.ToLower())
                    || x.Author.Username.ToLower().Contains(keyword.ToLower())
                    || x.Url.ToLower().Contains(keyword.ToLower());
            return query.IncludeData
                    ? await _context.Posts.Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category)
                        .Where(wherePredicate).Skip(query.PageNumber - 1).Take(query.PageCount).ToListAsync()
                    : await _context.Posts.Where(wherePredicate).Skip(query.PageNumber - 1).Take(query.PageCount).ToListAsync();
        }
    }
}
