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
    public class DeletePostCommand : CommandBase, ICreatePostCommand<int>
    {
        private readonly BlogFilesContext _blogFilesContext;
        public DeletePostCommand(BlogContext context) : base(context)
        {
        }

        public DeletePostCommand(BlogContext context, BlogFilesContext blogFilesContext) 
            : this(context)
        {
            _blogFilesContext = blogFilesContext;
        }

        public int Id { get; set; }
        public Guid FileId { get; set; }
        public ICollection<int> TagIds { get; set; }

        public int Handle()
        {
            int returnValue = 0;
            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    DeleteFileCommand deleteFileCommand =
                        new DeleteFileCommand(_blogFilesContext,
                            transaction.GetDbTransaction())
                        {
                            Id = FileId
                        };

                    returnValue = deleteFileCommand.Handle();

                    DeletePost();
                    returnValue = Context.SaveChanges();

                    DeleteTag();
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
                    DeleteFileCommand deleteFileCommand =
                        new DeleteFileCommand(_blogFilesContext,
                            transaction.GetDbTransaction())
                        {
                            Id = FileId
                        };

                    returnValue = await deleteFileCommand.HandleAsync();

                    DeletePost();
                    returnValue = await Context.SaveChangesAsync();

                    DeleteTag();
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

        private void DeletePost()
        {
            var post = Context.Posts.SingleOrDefault(m => m.Id == Id);
            Context.Posts.Remove(post);
        }

        private void DeleteTag()
        {
            var tagPostsToBeDeleted = Context.TagPosts.Where(x => x.PostId.Equals(Id));
            Context.RemoveRange(tagPostsToBeDeleted);
        }
    }
}
