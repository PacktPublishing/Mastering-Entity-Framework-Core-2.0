using MasteringEFCore.Concurrencies.Starter.Core.Queries.Posts;
using MasteringEFCore.Concurrencies.Starter.Data;
using MasteringEFCore.Concurrencies.Starter.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Concurrencies.Starter.Infrastructure.Queries.Posts
{
    public class GetPaginatedPostQuery
        : QueryBase, IGetPaginatedPostQuery<GetPaginatedPostQuery>
    {
        public GetPaginatedPostQuery(BlogContext context) : base(context)
        {
        }

        public int PageNumber { get; set; }
        public int PageCount { get; set; }
        public bool IncludeData { get; set; }

        public IEnumerable<Post> Handle()
        {
            var data = IncludeData
                ? Context.Posts.Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category)
                    .AsQueryable()
                    .Skip(PageNumber - 1).Take(PageCount)
                    .ToList()
                : Context.Posts
                    .AsQueryable()
                    .Skip(PageNumber - 1).Take(PageCount)
                    .ToList();

            data.ForEach(item =>
            {
                item.Author = item.Author ?? new User();
                item.Category = item.Category ?? new Category();
                item.TagPosts = item.TagPosts ?? new List<TagPost>();
                item.Comments = item.Comments ?? new List<Comment>();
                item.Tags = item.Tags ?? new List<Tag>();
                item.TagIds = item.TagIds ?? new List<int>();
            });

            return data;
        }

        public async Task<IEnumerable<Post>> HandleAsync()
        {
            var data = IncludeData
                ? await Context.Posts.Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category)
                    .AsQueryable()
                    .Skip(PageNumber - 1).Take(PageCount)
                    .ToListAsync()
                : await Context.Posts
                    .AsQueryable()
                    .Skip(PageNumber - 1).Take(PageCount)
                    .ToListAsync();

            data.ForEach(item =>
            {
                item.Author = item.Author ?? new User();
                item.Category = item.Category ?? new Category();
                item.TagPosts = item.TagPosts ?? new List<TagPost>();
                item.Comments = item.Comments ?? new List<Comment>();
                item.Tags = item.Tags ?? new List<Tag>();
                item.TagIds = item.TagIds ?? new List<int>();
            });

            return data;
        }
    }
}
