using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.Concurrencies.Final.Data;
using MasteringEFCore.Concurrencies.Final.Core.Queries.Posts;
using MasteringEFCore.Concurrencies.Final.Core.Queries;
using MasteringEFCore.Concurrencies.Final.Models;
using Microsoft.EntityFrameworkCore;

namespace MasteringEFCore.Concurrencies.Final.Infrastructure.Queries.Posts
{
    public class GetAllPostsQuery : QueryBase, IGetAllPostsQuery<GetAllPostsQuery>
    {
        public GetAllPostsQuery(BlogContext context) : base(context)
        {
        }

        public bool IncludeData { get; set; }

        public IEnumerable<Post> Handle()
        {
            var posts = IncludeData
                        ? Context.Posts
                        .Include(p => p.Author).Include(p => p.Blog)
                        .Include(p => p.Category).Include(p=>p.TagPosts)
                        .ToList()
                        : Context.Posts
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
                        .Include(p => p.Author).Include(p => p.Blog)
                        .Include(p => p.Category).Include(p=>p.TagPosts)
                        .ToListAsync()
                        : await Context.Posts
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
