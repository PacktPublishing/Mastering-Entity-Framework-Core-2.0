using MasteringEFCore.MultiTenancy.Final.Core.Queries.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.MultiTenancy.Final.Models;
using MasteringEFCore.MultiTenancy.Final.Data;
using Microsoft.EntityFrameworkCore;

namespace MasteringEFCore.MultiTenancy.Final.Infrastructure.Queries.Posts
{
    public class GetRecentPostQuery : QueryBase, IGetRecentPostQuery<GetRecentPostQuery>
    {
        public GetRecentPostQuery(BlogContext context) : base(context)
        {
        }

        public int Size { get; set; }
        public bool IncludeData { get; set; }

        public IEnumerable<Post> Handle()
        {
            var posts = IncludeData
                        ? Context.Posts
                            .Include(p => p.Author).Include(p => p.Blog)
                            .Include(p => p.Category).Include(p => p.TagPosts)
                            .OrderByDescending(x => x.CreatedAt).Take(Size)
                            .ToList()
                        : Context.Posts
                            .OrderByDescending(x => x.CreatedAt).Take(Size)
                            .ToList();
            posts.ForEach(x =>
            {
                var tags = (from tag in Context.Tags
                            join tagPost in Context.TagPosts
                            on tag.Id equals tagPost.TagId
                            where tagPost.PostId == x.Id
                            select tag).ToList();
                x.TagNames = string.Join(", ", tags.Select(y => y.Name).ToArray());
            });

            return posts;
        }

        public async Task<IEnumerable<Post>> HandleAsync()
        {
            var posts = IncludeData
                        ? await Context.Posts
                            .OrderByDescending(x => x.CreatedAt).Take(Size)
                            .Include(p => p.Author).Include(p => p.Blog)
                            .Include(p => p.Category).Include(p => p.TagPosts)
                            .ToListAsync()
                        : await Context.Posts
                            .OrderByDescending(x => x.CreatedAt).Take(Size)
                            .ToListAsync();
            posts.ForEach(x =>
            {
                var tags = (from tag in Context.Tags
                            join tagPost in Context.TagPosts
                            on tag.Id equals tagPost.TagId
                            where tagPost.PostId == x.Id
                            select tag).ToList();
                x.TagNames = string.Join(", ", tags.Select(y => y.Name).ToArray());
            });

            return posts;
        }
    }
}
