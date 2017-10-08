using MasteringEFCore.Transactions.Final.Core.Commands.Posts;
using MasteringEFCore.Transactions.Final.Data;
using MasteringEFCore.Transactions.Final.Helpers;
using MasteringEFCore.Transactions.Final.Infrastructure.Commands.Files;
using MasteringEFCore.Transactions.Final.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace MasteringEFCore.Transactions.Final.Infrastructure.Commands.Posts
{
    public class UpdatePostCommand : CommandBase, ICreatePostCommand<int>
    {
        private readonly BlogFilesContext _blogFilesContext;

        public UpdatePostCommand(BlogContext context) : base(context)
        {
        }

        public UpdatePostCommand(BlogContext context, BlogFilesContext blogFilesContext) 
            : this(context)
        {
            _blogFilesContext = blogFilesContext;
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
        public File File { get; set; }

        public int Handle()
        {
            int returnValue = 0;
            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    UpdateFileCommand updateFileCommand =
                        new UpdateFileCommand(_blogFilesContext,
                            transaction.GetDbTransaction())
                        {
                            Id = File.Id,
                            Name = File.Name,
                            FileName = File.FileName,
                            Content = File.Content,
                            Length = File.Length,
                            ContentType = File.ContentType,
                            ContentDisposition = File.ContentDisposition
                        };

                    returnValue = updateFileCommand.Handle();

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
                        FileId = File.Id
                    });
                    returnValue = Context.SaveChanges();

                    UpdateTags();
                    returnValue = Context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    ExceptionDispatchInfo.Capture(exception.InnerException).Throw();
                }
            }

            return returnValue;
        }

        public async Task<int> HandleAsync()
        {
            int returnValue = 0;
            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    UpdateFileCommand updateFileCommand =
                        new UpdateFileCommand(_blogFilesContext,
                            transaction.GetDbTransaction())
                        {
                            Id = File.Id,
                            Name = File.Name,
                            FileName = File.FileName,
                            Content = File.Content,
                            Length = File.Length,
                            ContentType = File.ContentType,
                            ContentDisposition = File.ContentDisposition
                        };

                    returnValue = await updateFileCommand.HandleAsync();

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
                        CreatedAt = DateTime.Now,
                        CreatedBy = AuthorId,
                        Url = Title.Generate()
                    });
                    returnValue = await Context.SaveChangesAsync();

                    UpdateTags();
                    returnValue = await Context.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    ExceptionDispatchInfo.Capture(exception.InnerException).Throw();
                }
            }

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
