using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MasteringEFCore.Transactions.Starter.Data;
using MasteringEFCore.Transactions.Starter.Models;
using Microsoft.AspNetCore.Authorization;
using MasteringEFCore.Transactions.Starter.Repositories;
using MasteringEFCore.Transactions.Starter.Infrastructure.Commands.Posts;
using MasteringEFCore.Transactions.Starter.Infrastructure.Queries.Posts;
using ExpressionPostQueries = MasteringEFCore.Transactions.Starter.Infrastructure.QueriesWithExpressions.Posts;

namespace MasteringEFCore.Transactions.Starter.Controllers
{
    //[Authorize]
    public class PostsController : Controller
    {
        private readonly BlogContext _context;
        private readonly IPostRepository _postRepository;

        public PostsController(BlogContext context, IPostRepository repository)
        {
            _context = context;
            _postRepository = repository;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            return View(await _postRepository.GetAsync(
                new GetAllPostsQuery(_context)
                {
                    IncludeData = true
                }));
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
                    IncludeData = true
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
            if (ModelState.IsValid)
            {
                await _postRepository.ExecuteAsync(
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

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _postRepository.ExecuteAsync(
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
