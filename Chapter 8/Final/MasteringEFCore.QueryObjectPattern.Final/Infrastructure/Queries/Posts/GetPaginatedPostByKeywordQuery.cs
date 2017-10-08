using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.QueryObjectPattern.Final.Data;
using MasteringEFCore.QueryObjectPattern.Final.Models;
using System.Linq.Expressions;
using MasteringEFCore.QueryObjectPattern.Final.Core.Queries.Posts;
using Microsoft.EntityFrameworkCore;

namespace MasteringEFCore.QueryObjectPattern.Final.Infrastructure.Queries.Posts
{
    public class GetPaginatedPostByKeywordQuery : QueryBase, IGetPaginatedPostByKeywordQuery<GetPaginatedPostByKeywordQuery>
    {
        public GetPaginatedPostByKeywordQuery(BlogContext context) : base(context)
        {
        }

        public string Keyword { get; set; }
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
        public bool IncludeData { get; set; }

        public IEnumerable<Post> Handle()
        {
            var keyword = Keyword;
            Expression<Func<Post, bool>> wherePredicate = x => x.Title.ToLower().Contains(keyword.ToLower())
                                                                || x.Blog.Title.ToLower().Contains(keyword.ToLower())
                    || x.Blog.Subtitle.ToLower().Contains(keyword.ToLower())
                    || x.Category.Name.ToLower().Contains(keyword.ToLower())
                    || x.Content.ToLower().Contains(keyword.ToLower())
                    || x.Summary.ToLower().Contains(keyword.ToLower())
                    || x.Author.Username.ToLower().Contains(keyword.ToLower())
                    || x.Url.ToLower().Contains(keyword.ToLower());
            return IncludeData
                    ? Context.Posts.Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category)
                        .Where(wherePredicate).Skip(PageNumber - 1).Take(PageCount)
                        .ToList()
                    : Context.Posts.Where(wherePredicate).Skip(PageNumber - 1).Take(PageCount)
                    .ToList();
        }

        public async Task<IEnumerable<Post>> HandleAsync()
        {
            var keyword = Keyword;
            Expression<Func<Post, bool>> wherePredicate = x => x.Title.ToLower().Contains(keyword.ToLower())
                                                                || x.Blog.Title.ToLower().Contains(keyword.ToLower())
                    || x.Blog.Subtitle.ToLower().Contains(keyword.ToLower())
                    || x.Category.Name.ToLower().Contains(keyword.ToLower())
                    || x.Content.ToLower().Contains(keyword.ToLower())
                    || x.Summary.ToLower().Contains(keyword.ToLower())
                    || x.Author.Username.ToLower().Contains(keyword.ToLower())
                    || x.Url.ToLower().Contains(keyword.ToLower());
            return IncludeData
                    ? await Context.Posts.Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category)
                        .Where(wherePredicate).Skip(PageNumber - 1).Take(PageCount)
                        .ToListAsync()
                    : await Context.Posts.Where(wherePredicate).Skip(PageNumber - 1).Take(PageCount)
                    .ToListAsync();
        }
    }
}
