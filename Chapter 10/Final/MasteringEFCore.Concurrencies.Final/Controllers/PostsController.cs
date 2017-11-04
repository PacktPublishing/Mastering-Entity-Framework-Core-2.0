using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MasteringEFCore.Concurrencies.Final.Data;
using MasteringEFCore.Concurrencies.Final.Models;
using Microsoft.AspNetCore.Authorization;
using MasteringEFCore.Concurrencies.Final.Repositories;
using MasteringEFCore.Concurrencies.Final.Infrastructure.Commands.Posts;
using MasteringEFCore.Concurrencies.Final.Infrastructure.Queries.Posts;
using ExpressionPostQueries = MasteringEFCore.Concurrencies.Final.Infrastructure.QueriesWithExpressions.Posts;
using System.IO;
using Microsoft.AspNetCore.Http;
using MasteringEFCore.Concurrencies.Final.Infrastructure.Commands.Files;
using MasteringEFCore.Concurrencies.Final.Infrastructure.Commands;
using System.Runtime.ExceptionServices;
using MasteringEFCore.Concurrencies.Final.Infrastructure.Queries.Files;

namespace MasteringEFCore.Concurrencies.Final.Controllers
{
    //[Authorize]
    [Route("Admin/Posts")]
    public class PostsController : Controller
    {
        private readonly BlogContext _context;
        private readonly BlogFilesContext _filesContext;
        private readonly IPostRepository _postRepository;
        private readonly IFileRepository _fileRepository;

        public PostsController(BlogContext context, BlogFilesContext filesContext, 
            IPostRepository repository, IFileRepository fileRepository)
        {
            _context = context;
            _filesContext = filesContext;
            _postRepository = repository;
            _fileRepository = fileRepository;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var posts = await _postRepository.GetAsync(
                new GetAllPostsQuery(_context)
                {
                    IncludeData = true
                });
            posts.ToList().ForEach(item =>
            {
                var file = _fileRepository.GetSingle(
                new GetFileByIdQuery(_filesContext)
                {
                    Id = item.FileId
                });
                if (file != null)
                {
                    item.FileName = file.FileName;
                }
            });
            return View(posts);
        }

        [Route("~/blog")]
        public async Task<IActionResult> GetPostsBlogHome(int pageNumber, int pageCount)
        {
            var results = await _postRepository.GetAsync(
                new GetPaginatedPostQuery(_context)
                {
                    IncludeData = true,
                    PageCount = pageCount,
                    PageNumber = pageNumber
                });
            return View(results);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetPaginatedPosts(string keyword, int pageNumber, int pageCount)
        {
            var results = await _postRepository.GetAsync(
                new ExpressionPostQueries.GetPaginatedPostByKeywordQuery(_context)
                {
                    IncludeData = true,
                    Keyword = keyword,
                    PageCount = pageCount,
                    PageNumber = pageNumber
                });
            return Ok(results);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetPostsByAuthor(string author)
        {
            var results = await _postRepository.GetAsync(
                new ExpressionPostQueries.GetPostByAuthorQuery(_context)
                {
                    IncludeData = true,
                    Author = author
                });
            return Ok(results);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetPostsByCategory(string category)
        {
            var results = await _postRepository.GetAsync(
                new ExpressionPostQueries.GetPostByCategoryQuery(_context)
                {
                    IncludeData = true,
                    Category = category
                });
            return Ok(results);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetPostByHighestVisitors()
        {
            var results = await _postRepository.GetAsync(
                new GetPostByHighestVisitorsQuery(_context)
                {
                    IncludeData = true
                });
            return Ok(results);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetPostByPublishedYear(int year)
        {
            var results = await _postRepository.GetAsync(
                new ExpressionPostQueries.GetPostByPublishedYearQuery(_context)
                {
                    IncludeData = true,
                    Year = year
                });
            return Ok(results);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetPostByTitle(string title)
        {
            var results = await _postRepository.GetAsync(
                new ExpressionPostQueries.GetPostByTitleQuery(_context)
                {
                    IncludeData = true,
                    Title = title
                });
            return Ok(results);
        }

        [Route("~/Posts/Display")]
        public async Task<IActionResult> Display(string id)
        {
            var url = id;
            if (url == null)
            {
                return NotFound();
            }

            var post = await _postRepository.GetSingleAsync(
                new GetPostByUrlQuery(_context)
                {
                    IncludeData = true,
                    Url = url
                });
            if (post == null)
            {
                return NotFound();
            }

            if (!post.FileId.Equals(Guid.Empty))
            {
                var file = await _fileRepository.GetSingleAsync(
                    new GetFileByIdQuery(_filesContext)
                    {
                        Id = post.FileId
                    });
                if (file != null)
                {
                    post.FileName = file.FileName;
                }
            }

            post.Author = post.Author ?? new User();
            post.Category = post.Category ?? new Category();
            post.TagPosts = post.TagPosts ?? new List<TagPost>();
            post.Comments = post.Comments ?? new List<Comment>();
            post.Tags = post.Tags ?? new List<Tag>();
            post.TagIds = post.TagIds ?? new List<int>();

            return View(post);
        }

        [HttpGet]
        public ActionResult GetCommentsListViewComponent(string postId)
        {
            return ViewComponent("CommentsListViewComponent", postId);
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var post = await _postRepository.GetSingleAsync(
                new GetPostByIdQuery(_context)
                {
                    IncludeData = true,
                    Id = id
                });
            if (post == null)
            {
                return NotFound();
            }

            if (!post.FileId.Equals(Guid.Empty))
            {
                var file = await _fileRepository.GetSingleAsync(
                    new GetFileByIdQuery(_filesContext)
                    {
                        Id = post.FileId
                    });
                if (file != null)
                {
                    post.FileName = file.FileName;
                }
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["BlogId"] = new SelectList(_context.Blogs, "Id", "Url");
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Id");
            ViewData["TagIds"] = new MultiSelectList(_context.Tags, "Id", "Name");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,Summary," +
            "PublishedDateTime,Url,VisitorCount,CreatedAt,ModifiedAt,BlogId," +
            "AuthorId,CategoryId,TagIds")] Post post, IFormFile headerImage)
        {
            if (ModelState.IsValid)
            {
                Models.File file = null;
                if (headerImage != null || headerImage.ContentType.ToLower().StartsWith("image/"))
                {
                    MemoryStream ms = new MemoryStream();
                    headerImage.OpenReadStream().CopyTo(ms);

                    file = new Models.File()
                    {
                        Id = Guid.NewGuid(),
                        Name = headerImage.Name,
                        FileName = Path.GetFileName(headerImage.FileName),
                        Content = ms.ToArray(),
                        Length = headerImage.Length,
                        ContentType = headerImage.ContentType
                    };
                }

                var transactions = new TransactionScope();
                try
                {
                    if (file != null)
                    {
                        transactions.Transactions.Add(_filesContext.Database.BeginTransaction());
                        await _fileRepository.ExecuteAsync(
                            new CreateFileCommand(_filesContext)
                            {
                                Content = file.Content,
                                ContentDisposition = file.ContentDisposition,
                                ContentType = file.ContentType,
                                FileName = file.FileName,
                                Id = file.Id,
                                Length = file.Length,
                                Name = file.Name
                            });
                    }

                    transactions.Transactions.Add(_context.Database.BeginTransaction());
                    await _postRepository.ExecuteAsync(
                        new CreatePostCommand(_context)
                        {
                            Title = post.Title,
                            Summary = post.Summary,
                            Content = post.Content,
                            PublishedDateTime = post.PublishedDateTime,
                            AuthorId = post.AuthorId,
                            BlogId = post.BlogId,
                            CategoryId = post.CategoryId,
                            TagIds = post.TagIds,
                            FileId = file.Id
                        });
                    transactions.Commit();
                }
                catch (Exception exception)
                {
                    transactions.Rollback();
                    ExceptionDispatchInfo.Capture(exception.InnerException).Throw();
                }
                return RedirectToAction("Index");
            }
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id", post.AuthorId);
            ViewData["BlogId"] = new SelectList(_context.Blogs, "Id", "Url", post.BlogId);
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Id", post.CategoryId);
            ViewData["TagIds"] = new MultiSelectList(_context.Tags, "Id", "Name", post.TagIds);
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postRepository.GetSingleAsync(
                new GetPostByIdQuery(_context)
                {
                    IncludeData = false,
                    Id = id
                });
            if (post == null)
            {
                return NotFound();
            }
            if (!post.FileId.Equals(Guid.Empty))
            {
                var file = await _fileRepository.GetSingleAsync(
                    new GetFileByIdQuery(_filesContext)
                    {
                        Id = post.FileId
                    });
                if (file != null)
                {
                    post.FileName = file.FileName;
                }
            }

            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id", post.AuthorId);
            ViewData["BlogId"] = new SelectList(_context.Blogs, "Id", "Url", post.BlogId);
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Id", post.CategoryId);
            ViewData["TagIds"] = new MultiSelectList(_context.Tags, "Id", "Name", post.TagIds);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,Summary," +
            "PublishedDateTime,Url,VisitorCount,CreatedAt,ModifiedAt,BlogId,AuthorId," +
            "CategoryId,TagIds,FileId")] Post post, IFormFile headerImage)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Models.File file = null;
                    if (headerImage != null || headerImage.ContentType.ToLower().StartsWith("image/"))
                    {
                        MemoryStream ms = new MemoryStream();
                        headerImage.OpenReadStream().CopyTo(ms);

                        file = new Models.File()
                        {
                            Id = post.FileId,
                            Name = headerImage.Name,
                            FileName = Path.GetFileName(headerImage.FileName),
                            Content = ms.ToArray(),
                            Length = headerImage.Length,
                            ContentType = headerImage.ContentType
                        };
                    }

                    var transactions = new TransactionScope();
                    try
                    {
                        if (file != null)
                        {
                            transactions.Transactions.Add(_filesContext.Database.BeginTransaction());
                            await _fileRepository.ExecuteAsync(
                                new UpdateFileCommand(_filesContext)
                                {
                                    Content = file.Content,
                                    ContentDisposition = file.ContentDisposition,
                                    ContentType = file.ContentType,
                                    FileName = file.FileName,
                                    Id = file.Id,
                                    Length = file.Length,
                                    Name = file.Name
                                });
                        }

                        transactions.Transactions.Add(_context.Database.BeginTransaction());
                        await _postRepository.ExecuteAsync(
                            new UpdatePostCommand(_context)
                            {
                                Id = post.Id,
                                Title = post.Title,
                                Summary = post.Summary,
                                Content = post.Content,
                                PublishedDateTime = post.PublishedDateTime,
                                AuthorId = post.AuthorId,
                                BlogId = post.BlogId,
                                CategoryId = post.CategoryId,
                                TagIds = post.TagIds,
                                FileId = file.Id
                            });
                        transactions.Commit();
                    }
                    catch (Exception exception)
                    {
                        transactions.Rollback();
                        ExceptionDispatchInfo.Capture(exception.InnerException).Throw();
                    }
                }
                catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        var transactions = new TransactionScope();
                        try
                        {
                            foreach (var entry in dbUpdateConcurrencyException.Entries)
                            {
                                if (entry.Entity is Post)
                                {
                                    var postToBeUpdated = await _postRepository.GetSingleAsync(
                                        new GetPostByIdQuery(_context) { Id = ((Post)entry.Entity).Id }
                                        );
                                    var postFromDatabase = _context.Entry(postToBeUpdated);

                                    foreach (var property in entry.Metadata.GetProperties())
                                    {
                                        var proposedValue = entry.Property(property.Name).CurrentValue;
                                        var originalValue = entry.Property(property.Name).OriginalValue;
                                        var databaseValue = postFromDatabase.Property(property.Name).CurrentValue;

                                        // entry.Property(property.Name).CurrentValue = <value to be saved>;

                                        // Update original values to
                                        entry.Property(property.Name).OriginalValue = postFromDatabase.Property(property.Name).CurrentValue;
                                    }
                                }
                                else if (entry.Entity is Models.File)
                                {
                                    var fileToBeUpdated = await _fileRepository.GetSingleAsync(
                                        new GetFileByIdQuery(_filesContext) { Id = ((Models.File)entry.Entity).Id }
                                        );
                                    var fileFromDatabase = _context.Entry(fileToBeUpdated);

                                    #region Tracking changes in columns and storing respective data
                                    foreach (var property in entry.Metadata.GetProperties())
                                    {
                                        var proposedValue = entry.Property(property.Name).CurrentValue;
                                        var originalValue = entry.Property(property.Name).OriginalValue;
                                        var databaseValue = fileFromDatabase.Property(property.Name).CurrentValue;

                                        if (property.Name.Equals("Timestamp"))
                                        {
                                            entry.Property(property.Name).OriginalValue = fileFromDatabase.Property(property.Name).CurrentValue;
                                        }

                                        if (originalValue.Equals(databaseValue) && !proposedValue.Equals(originalValue))
                                        {
                                            // If the changes from database and original are same, 
                                            // then we will save the entity with proposed value.
                                            // It means another user had updated the column 
                                            // the latest change from datastore will be retained
                                            entry.Property(property.Name).CurrentValue = proposedValue;
                                        }
                                        else if (!originalValue.Equals(databaseValue) && proposedValue.Equals(originalValue))
                                        {
                                            // If the proposed value and original value are same,
                                            // then we will update the entity with the database value
                                            // It means the current user from application\UI had updated 
                                            // the column and we were retaining it
                                            entry.Property(property.Name).CurrentValue = databaseValue;
                                        }
                                        else if (!originalValue.Equals(databaseValue) && !proposedValue.Equals(originalValue))
                                        {
                                            // Latest changes from application\UI will be persisted, optimistic concurrency
                                            entry.Property(property.Name).CurrentValue = proposedValue;

                                            // Ignoring the latest changes from application\UI, pessimistic concurrency
                                        }
                                    }
                                    #endregion

                                    #region Configure EF Core to ignore any changes from the app\ui and retain data from datastore
                                    #endregion
                                }
                                else
                                {
                                    throw new NotSupportedException("Don't know how to handle concurrency conflicts for " + entry.Metadata.Name);
                                }
                            }
                            transactions.Commit();
                        }
                        catch (Exception exception)
                        {
                            transactions.Rollback();
                            ExceptionDispatchInfo.Capture(exception.InnerException).Throw();
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id", post.AuthorId);
            ViewData["BlogId"] = new SelectList(_context.Blogs, "Id", "Url", post.BlogId);
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Id", post.CategoryId);
            ViewData["TagIds"] = new MultiSelectList(_context.Tags, "Id", "Name", post.TagIds);
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var post = await _postRepository.GetSingleAsync(
                new GetPostByIdQuery(_context)
                {
                    IncludeData = true,
                    Id = id
                });
            if (post == null)
            {
                return NotFound();
            }

            if (!post.FileId.Equals(Guid.Empty))
            {
                var file = await _fileRepository.GetSingleAsync(
                    new GetFileByIdQuery(_filesContext)
                    {
                        Id = post.FileId
                    });
                if (file != null)
                {
                    post.FileName = file.FileName;
                }
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, Guid fileId)
        {
            var transactions = new TransactionScope();
            try
            {
                if (!fileId.Equals(Guid.Empty))
                {

                    transactions.Transactions.Add(_filesContext.Database.BeginTransaction());
                    await _fileRepository.ExecuteAsync(
                        new DeleteFileCommand(_filesContext)
                        {
                            Id = fileId
                        });
                }

                transactions.Transactions.Add(_context.Database.BeginTransaction());
                await _postRepository.ExecuteAsync(
                    new DeletePostCommand(_context)
                    {
                        Id = id
                    });
                transactions.Commit();
            }
            catch (Exception exception)
            {
                transactions.Rollback();
                ExceptionDispatchInfo.Capture(exception.InnerException).Throw();
            }

            return RedirectToAction("Index");
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
