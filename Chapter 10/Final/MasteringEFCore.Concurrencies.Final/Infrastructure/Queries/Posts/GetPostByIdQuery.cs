using MasteringEFCore.Concurrencies.Final.Core.Queries.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.Concurrencies.Final.Data;
using MasteringEFCore.Concurrencies.Final.Core.Queries;
using MasteringEFCore.Concurrencies.Final.Models;
using Microsoft.EntityFrameworkCore;

namespace MasteringEFCore.Concurrencies.Final.Infrastructure.Queries.Posts
{
    public class GetPostByIdQuery : QueryBase, IGetPostByIdQuery<GetPostByIdQuery>
    {
        public GetPostByIdQuery(BlogContext context) : base(context)
        {
        }

        public int? Id { get; set; }
        public bool IncludeData { get; set; }

        public Post Handle()
        {
            var post = IncludeData
                           ? Context.Posts.Include(p => p.Author)
                               .Include(p => p.Blog).Include(p => p.Category)
                               .SingleOrDefault(x => x.Id.Equals(Id))
                           : Context.Posts
                               .SingleOrDefault(x => x.Id.Equals(Id));

            return IncludeTags(post);
        }

        public async Task<Post> HandleAsync()
        {
            var post = IncludeData
                           ? await Context.Posts.Include(p => p.Author)
                               .Include(p => p.Blog).Include(p => p.Category)
                               .SingleOrDefaultAsync(x => x.Id.Equals(Id))
                           : await Context.Posts
                               .SingleOrDefaultAsync(x => x.Id.Equals(Id));

            return IncludeTags(post);
        }

        private Post IncludeTags(Post post)
        {
            int idValue = Id ?? 0;
            post.Tags = (from tag in Context.Tags
                         join tagPost in Context.TagPosts
                         on tag.Id equals tagPost.TagId
                         where tagPost.PostId == idValue
                         select tag).ToList();
            post.TagIds = post.Tags.Select(x => x.Id).ToList();
            post.TagNames = string.Join(", ", post.Tags.Select(x => x.Name).ToArray());

            return post;
        }
    }
}
