using MasteringEFCore.Transactions.Final.Core.Commands.Posts;
using MasteringEFCore.Transactions.Final.Helpers;
using MasteringEFCore.Transactions.Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.Transactions.Final.Data;
using System.Runtime.ExceptionServices;
using Microsoft.EntityFrameworkCore.Storage;
using MasteringEFCore.Transactions.Final.Infrastructure.Commands.Files;

namespace MasteringEFCore.Transactions.Final.Infrastructure.Commands.Posts
{
    public class CreatePostCommand : CommandBase, ICreatePostCommand<int>
    {
        private readonly BlogFilesContext _blogFilesContext;

        public CreatePostCommand(BlogContext context) : base(context)
        {
        }

        public CreatePostCommand(BlogContext context, BlogFilesContext blogFilesContext) 
            : this(context)
        {
            _blogFilesContext = blogFilesContext;
        }

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
                    CreateFileCommand createFileCommand =
                        new CreateFileCommand(_blogFilesContext,
                            transaction.GetDbTransaction())
                        {
                            Id = Guid.NewGuid(),
                            Name = File.Name,
                            FileName = File.FileName,
                            Content = File.Content,
                            Length = File.Length,
                            ContentType = File.ContentType,
                            ContentDisposition = File.ContentDisposition
                        };

                    returnValue = createFileCommand.Handle();

                    Post post = new Post
                    {
                        Title = Title,
                        Content = Content,
                        Summary = Summary,
                        BlogId = BlogId,
                        AuthorId = AuthorId,
                        CategoryId = CategoryId,
                        PublishedDateTime = PublishedDateTime,
                        CreatedAt = DateTime.Now,
                        CreatedBy = AuthorId,
                        Url = Title.Generate(),
                        FileId = File.Id
                    };

                    Context.Add(post);
                    returnValue = Context.SaveChanges();

                    foreach (int tagId in TagIds)
                    {
                        Context.Add(new TagPost
                        {
                            TagId = tagId,
                            Post = post
                        });
                    }
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
            int returnValue = 0;using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    CreateFileCommand createFileCommand =
                        new CreateFileCommand(_blogFilesContext,
                            transaction.GetDbTransaction())
                        {
                            Id = Guid.NewGuid(),
                            Name = File.Name,
                            FileName = File.FileName,
                            Content = File.Content,
                            Length = File.Length,
                            ContentType = File.ContentType,
                            ContentDisposition = File.ContentDisposition
                        };

                    returnValue = await createFileCommand.HandleAsync();

                    Post post = new Post
                    {
                        Title = Title,
                        Content = Content,
                        Summary = Summary,
                        BlogId = BlogId,
                        AuthorId = AuthorId,
                        CategoryId = CategoryId,
                        PublishedDateTime = PublishedDateTime,
                        CreatedAt = DateTime.Now,
                        CreatedBy = AuthorId,
                        Url = Title.Generate(),
                        FileId = File.Id
                    };

                    Context.Add(post);
                    returnValue = await Context.SaveChangesAsync();

                    foreach (int tagId in TagIds)
                    {
                        Context.Add(new TagPost
                        {
                            TagId = tagId,
                            Post = post
                        });
                    }

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
    }
}
