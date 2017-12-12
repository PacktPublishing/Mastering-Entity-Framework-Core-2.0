using MasteringEFCore.MultiTenancy.Final.Core.Queries.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.MultiTenancy.Final.Data;
using MasteringEFCore.MultiTenancy.Final.Core.Queries;
using MasteringEFCore.MultiTenancy.Final.Models;
using Microsoft.EntityFrameworkCore;

namespace MasteringEFCore.MultiTenancy.Final.Infrastructure.Queries.Posts
{
    public class GetPostByUrlQuery : QueryBase, IGetPostByUrlQuery<GetPostByUrlQuery>
    {
        public GetPostByUrlQuery(BlogContext context) : base(context)
        {
        }

        public string Url { get; set; }
        public bool IncludeData { get; set; }

        public Post Handle()
        {
            var post = IncludeData
                           ? Context.Posts.Include(p => p.Author)
                               .Include(p => p.Blog).Include(p => p.Category)
                               .IgnoreQueryFilters()
                               .SingleOrDefault(x => x.Url.Equals(Url))
                           : Context.Posts
                               .IgnoreQueryFilters()
                               .SingleOrDefault(x => x.Url.Equals(Url));

            return IncludeTags(post);
        }

        public async Task<Post> HandleAsync()
        {
            var post = IncludeData
                           ? await Context.Posts.Include(p => p.Author)
                               .Include(p => p.Blog).Include(p => p.Category)
                               .IgnoreQueryFilters()
                               .SingleOrDefaultAsync(x => x.Url.Equals(Url))
                           : await Context.Posts
                               .IgnoreQueryFilters()
                               .SingleOrDefaultAsync(x => x.Url.Equals(Url));

            return IncludeTags(post);
        }

        private Post IncludeTags(Post post)
        {
            post.Tags = (from tag in Context.Tags
                         join tagPost in Context.TagPosts
                         on tag.Id equals tagPost.TagId
                         where tagPost.PostId == post.Id
                         select tag).ToList();
            post.TagIds = post.Tags.Select(x => x.Id).ToList();
            post.TagNames = string.Join(", ", post.Tags.Select(x => x.Name).ToArray());

            return post;
        }
    }
}
