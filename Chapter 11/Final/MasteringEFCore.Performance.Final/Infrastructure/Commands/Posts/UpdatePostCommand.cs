using MasteringEFCore.Performance.Final.Core.Commands.Posts;
using MasteringEFCore.Performance.Final.Data;
using MasteringEFCore.Performance.Final.Helpers;
using MasteringEFCore.Performance.Final.Infrastructure.Commands.Files;
using MasteringEFCore.Performance.Final.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace MasteringEFCore.Performance.Final.Infrastructure.Commands.Posts
{
    public class UpdatePostCommand : CommandBase, ICreatePostCommand<int>
    {
        public UpdatePostCommand(BlogContext context) : base(context)
        {
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public int BlogId { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public ICollection<int> TagIds { get; set; }
        public DateTime PublishedDateTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public byte[] Timestamp { get; set; }
        public Guid FileId { get; set; }

        public int Handle()
        {
            int returnValue = 0;
            try
            {
                Context.Update(new Post
                {
                    Id = Id,
                    Title = Title,
                    Content = Content,
                    Summary = Summary,
                    BlogId = BlogId,
                    AuthorId = AuthorId,
                    CategoryId = CategoryId,
                    PublishedDateTime = PublishedDateTime,
                    ModifiedAt = DateTime.Now,
                    ModifiedBy = AuthorId,
                    Url = Title.Generate(),
                    Timestamp = Timestamp,
                    FileId = FileId
                });
                returnValue = Context.SaveChanges();

                UpdateTags();
                returnValue = Context.SaveChanges();
            }
            catch (Exception exception)
            {
                ExceptionDispatchInfo.Capture(exception.InnerException).Throw();
            }

            return returnValue;
        }

        public async Task<int> HandleAsync()
        {
            int returnValue = 0;
            //try
            //{
            //var post = Context.Posts.FirstOrDefault(item => item.Id.Equals(Id));
            //if (post != null)
            //{
            //    post.Title = Title;
            //    post.Content = Content;
            //    post.Summary = Summary;
            //    post.BlogId = BlogId;
            //    post.AuthorId = AuthorId;
            //    post.CategoryId = CategoryId;
            //    post.PublishedDateTime = PublishedDateTime;
            //    post.CreatedAt = CreatedAt;
            //    post.CreatedBy = AuthorId;
            //    post.ModifiedAt = DateTime.Now;
            //    post.ModifiedBy = AuthorId;
            //    post.Url = Title.Generate();
            //    post.FileId = FileId;
            //}
            Context.Update(new Post
            {
                Id = Id,
                Title = Title,
                Content = Content,
                Summary = Summary,
                BlogId = BlogId,
                AuthorId = AuthorId,
                CategoryId = CategoryId,
                PublishedDateTime = PublishedDateTime,
                ModifiedAt = DateTime.Now,
                ModifiedBy = AuthorId,
                Url = Title.Generate(),
                Timestamp = Timestamp,
                FileId = FileId
            });
            //Context.Entry(post).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            returnValue = await Context.SaveChangesAsync();

            UpdateTags();
            returnValue = await Context.SaveChangesAsync();
            //}
            //catch (Exception exception)
            //{
            //    ExceptionDispatchInfo.Capture(exception.InnerException).Throw();
            //}

            return returnValue;
        }

        private void UpdateTags()
        {
            var existingTagPosts = Context.TagPosts.Where(x => x.PostId.Equals(Id));
            var existingTagPostIds = existingTagPosts.Select(x => x.TagId);
            var tagPostIdsToBeDeleted = existingTagPostIds.Except(TagIds);
            var tagPostsToBeDeleted = Context.TagPosts.Where(x => x.PostId.Equals(Id)
                                        && tagPostIdsToBeDeleted.Contains(x.TagId));

            Context.RemoveRange(tagPostsToBeDeleted);

            var tagPostIdsToBeAdded = TagIds.Except(existingTagPostIds);
            foreach (int tagId in tagPostIdsToBeAdded)
            {
                Context.Add(new TagPost
                {
                    TagId = tagId,
                    PostId = Id
                });
            }
        }
    }
}
