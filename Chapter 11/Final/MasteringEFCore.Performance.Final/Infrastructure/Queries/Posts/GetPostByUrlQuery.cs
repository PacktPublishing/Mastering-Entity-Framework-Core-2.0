using MasteringEFCore.Performance.Final.Core.Queries.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.Performance.Final.Data;
using MasteringEFCore.Performance.Final.Core.Queries;
using MasteringEFCore.Performance.Final.Models;
using Microsoft.EntityFrameworkCore;

namespace MasteringEFCore.Performance.Final.Infrastructure.Queries.Posts
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
                               .SingleOrDefault(x => x.Url.Equals(Url))
                           : Context.Posts
                               .SingleOrDefault(x => x.Url.Equals(Url));

            return IncludeTags(post);
        }

        public async Task<Post> HandleAsync()
        {
            var post = IncludeData
                           ? await Context.Posts.Include(p => p.Author)
                               .Include(p => p.Blog).Include(p => p.Category)
                               .SingleOrDefaultAsync(x => x.Url.Equals(Url))
                           : await Context.Posts
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
