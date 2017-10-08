using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MasteringEFCore.QueryObjectPattern.Final.Data;
using MasteringEFCore.QueryObjectPattern.Final.Models;
using Microsoft.AspNetCore.Authorization;
using MasteringEFCore.QueryObjectPattern.Final.Repositories;
using MasteringEFCore.QueryObjectPattern.Final.Handlers;
using MasteringEFCore.QueryObjectPattern.Final.Infrastructure.Commands.Posts;
using MasteringEFCore.QueryObjectPattern.Final.Infrastructure.Queries.Posts;
using ExpressionPostQueries = MasteringEFCore.QueryObjectPattern.Final.Infrastructure.QueriesWithExpressions.Posts;

namespace MasteringEFCore.QueryObjectPattern.Final.Controllers
{
    //[Authorize]
    public class PostsController : Controller
    {
        private readonly BlogContext _context;
        //private readonly IPostRepository _repository;
        //private readonly IPostRepositoryWithQueries _repositoryWithQueries;
        //private readonly IPostDetailQueryHandler _postDetailQueryHandler;
        private readonly IPostRepositoryWithCommandsQueries _postRepositoryWithCommandsQueries;

        public PostsController(BlogContext context, IPostRepositoryWithCommandsQueries repositoryWithCommandsQueries)
        {
            _context = context;
            _postRepositoryWithCommandsQueries = repositoryWithCommandsQueries;
            //_repository = repository;
            //_repositoryWithQueries = repositoryWithQueries;
            //_postDetailQueryHandler = postDetailQueryHandler;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            //return View(await _repository.GetAllPostsAsync());
            //return View(await _repositoryWithQueries.GetAsync(new GetAllPostsQuery(true)));
            return View(await _postRepositoryWithCommandsQueries.GetAsync(
                new GetAllPostsQuery(_context)
                {
                    IncludeData = true
                }));
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetPaginatedPosts(string keyword, int pageNumber, int pageCount)
        {
            //var results = await _repositoryWithQueries.GetAsync(
            //    new GetPaginatedPostByKeywordQuery(keyword, pageNumber, pageCount, true));
            //var results = await _postRepositoryWithCommandsQueries.GetAsync(
            //    new GetPaginatedPostByKeywordQuery(_context)
            //    {
            //        IncludeData = true,
            //        Keyword = keyword,
            //        PageCount = pageCount,
            //        PageNumber = pageNumber
            //    });
            var results = await _postRepositoryWithCommandsQueries.GetAsync(
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
            //var results = await _repositoryWithQueries.GetAsync(
            //    new GetPostByAuthorQuery(author, true));
            //var results = await _postRepositoryWithCommandsQueries.GetAsync(
            //    new GetPostByAuthorQuery(_context)
            //    {
            //        IncludeData = true,
            //        Author = author
            //    });
            var results = await _postRepositoryWithCommandsQueries.GetAsync(
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
            //var results = await _repositoryWithQueries.GetAsync(
            //    new GetPostByCategoryQuery(category, true));
            //var results = await _postRepositoryWithCommandsQueries.GetAsync(
            //    new GetPostByCategoryQuery(_context)
            //    {
            //        IncludeData = true,
            //        Category = category
            //    });
            var results = await _postRepositoryWithCommandsQueries.GetAsync(
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
            //var results = await _repositoryWithQueries.GetAsync(
            //    new GetPostByHighestVisitorsQuery(true));
            var results = await _postRepositoryWithCommandsQueries.GetAsync(
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
            //var results = await _repositoryWithQueries.GetAsync(
            //    new GetPostByPublishedYearQuery(year, true));
            //var results = await _postRepositoryWithCommandsQueries.GetAsync(
            //    new GetPostByPublishedYearQuery(_context)
            //    {
            //        IncludeData = true
            //    });
            var results = await _postRepositoryWithCommandsQueries.GetAsync(
                new ExpressionPostQueries.GetPostByPublishedYearQuery(_context)
                {
                    IncludeData = true
                });
            return Ok(results);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetPostByTitle(string title)
        {
            //var results = await _repositoryWithQueries.GetAsync(
            //    new GetPostByTitleQuery(title, true));
            //var results = await _postRepositoryWithCommandsQueries.GetAsync(
            //    new GetPostByTitleQuery(_context)
            //    {
            //        IncludeData = true
            //    });
            var results = await _postRepositoryWithCommandsQueries.GetAsync(
                new ExpressionPostQueries.GetPostByTitleQuery(_context)
                {
                    IncludeData = true
                });
            return Ok(results);
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var post = await _context.Posts
            //    .Include(p => p.Author)
            //    .Include(p => p.Blog)
            //    .Include(p => p.Category)
            //    .FirstOrDefaultAsync(x => x.Id == id);

            //var post = await _repository.GetPostByIdAsync(id.Value);
            //var post = await _repositoryWithQueries.GetSingleAsync(new GetPostByIdQuery(id, true));
            //var post = await _postDetailQueryHandler.Handle(new PostDetailQuery(id));
            var post = await _postRepositoryWithCommandsQueries.GetSingleAsync(
                new GetPostByIdQuery(_context)
                {
                    IncludeData = true,
                    Id = id
                });
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["BlogId"] = new SelectList(_context.Blogs, "Id", "Url");
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Id");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,Summary,PublishedDateTime,Url,VisitorCount,CreatedAt,ModifiedAt,BlogId,AuthorId,CategoryId")] Post post)
        {
            //var results = new List<ValidationResult>();
            //var isBusinessValid = Validator.TryValidateObject(post, new ValidationContext(post, null, null), results, false);
            if (ModelState.IsValid)
            {
                //await _repository.AddPostAsync(post);
                await _postRepositoryWithCommandsQueries.ExecuteAsync(
                    new CreatePostCommand(_context)
                    {
                        Title = post.Title,
                        Summary = post.Summary,
                        Content = post.Content,
                        PublishedDateTime = post.PublishedDateTime,
                        AuthorId = post.AuthorId,
                        BlogId = post.BlogId,
                        CategoryId = post.CategoryId
                    });
                return RedirectToAction("Index");
            }
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id", post.AuthorId);
            ViewData["BlogId"] = new SelectList(_context.Blogs, "Id", "Url", post.BlogId);
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Id", post.CategoryId);
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var post = await _repositoryWithQueries.GetSingleAsync(new GetPostByIdQuery(id, false));
            //var post = await _repository.GetPostByIdAsync(id, false);
            var post = await _postRepositoryWithCommandsQueries.GetSingleAsync(
                new GetPostByIdQuery(_context)
                {
                    IncludeData = false,
                    Id = id
                });
            if (post == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id", post.AuthorId);
            ViewData["BlogId"] = new SelectList(_context.Blogs, "Id", "Url", post.BlogId);
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Id", post.CategoryId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,Summary,PublishedDateTime,Url,VisitorCount,CreatedAt,ModifiedAt,BlogId,AuthorId,CategoryId")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //await _repository.UpdatePostAsync(post);
                    await _postRepositoryWithCommandsQueries.ExecuteAsync(
                        new UpdatePostCommand(_context)
                        {
                            Id = post.Id,
                            Title = post.Title,
                            Summary = post.Summary,
                            Content = post.Content,
                            PublishedDateTime = post.PublishedDateTime,
                            AuthorId = post.AuthorId,
                            BlogId = post.BlogId,
                            CategoryId = post.CategoryId
                        });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id", post.AuthorId);
            ViewData["BlogId"] = new SelectList(_context.Blogs, "Id", "Url", post.BlogId);
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Id", post.CategoryId);
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var post = await _repositoryWithQueries.GetSingleAsync(new GetPostByIdQuery(id, true));
            //var post = await _repository.GetPostByIdAsync(id);
            var post = await _postRepositoryWithCommandsQueries.GetSingleAsync(
                new GetPostByIdQuery(_context)
                {
                    IncludeData = true,
                    Id = id
                });
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //await _repository.DeletePostAsync(id);
            await _postRepositoryWithCommandsQueries.ExecuteAsync(
                new DeletePostCommand(_context)
                {
                    Id = id
                });
            return RedirectToAction("Index");
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
