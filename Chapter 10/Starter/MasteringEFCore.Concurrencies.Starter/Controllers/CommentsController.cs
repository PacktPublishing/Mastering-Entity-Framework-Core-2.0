using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MasteringEFCore.Concurrencies.Starter.Data;
using MasteringEFCore.Concurrencies.Starter.Models;
using Microsoft.AspNetCore.Authorization;
using MasteringEFCore.Concurrencies.Starter.Repositories;
using MasteringEFCore.Concurrencies.Starter.Infrastructure.Commands.Comments;
using Microsoft.Extensions.Configuration;
using MasteringEFCore.Concurrencies.Starter.ViewModels;

namespace MasteringEFCore.Concurrencies.Starter.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly BlogContext _context;
        private readonly IConfigurationRoot _configuration;
        private readonly ICommentRepository _repository;

        public CommentsController(IConfigurationRoot configuration, BlogContext context, ICommentRepository repository)
        {
            _context = context;
            _repository = repository;
            _configuration = configuration;
        }

        // GET: Comments
        public async Task<IActionResult> Index()
        {
            var blogContext = _context.Comments.Include(c => c.Person).Include(c => c.Post).Include(c => c.User);
            return View(await blogContext.ToListAsync());
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Person)
                .Include(c => c.Post)
                .Include(c => c.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create
        public IActionResult Create()
        {
            ViewData["PersonId"] = new SelectList(_context.Set<Person>(), "Id", "Id");
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,CommentedAt,PostId,PersonId,UserId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["PersonId"] = new SelectList(_context.Set<Person>(), "Id", "Id", comment.PersonId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id", comment.PostId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", comment.UserId);
            return View(comment);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreatePostComment([FromBody] CommentViewModel comment)
        {
            var results = await _repository.ExecuteAsync(
                new CreateCommentCommand(_configuration, _context)
                {
                    PostId = comment.PostId,
                    Content = comment.Content,
                    Nickname = comment.Nickname
                });
            return View(comment);
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.SingleOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.Set<Person>(), "Id", "Id", comment.PersonId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id", comment.PostId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", comment.UserId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,CommentedAt,PostId,PersonId,UserId")] Comment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
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
            ViewData["PersonId"] = new SelectList(_context.Set<Person>(), "Id", "Id", comment.PersonId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id", comment.PostId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", comment.UserId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Person)
                .Include(c => c.Post)
                .Include(c => c.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _context.Comments.SingleOrDefaultAsync(m => m.Id == id);
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
