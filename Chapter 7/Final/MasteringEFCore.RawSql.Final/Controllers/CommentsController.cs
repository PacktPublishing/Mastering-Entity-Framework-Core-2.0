using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MasteringEFCore.RawSql.Final.Data;
using MasteringEFCore.RawSql.Final.Models;
using Microsoft.AspNetCore.Authorization;

namespace MasteringEFCore.RawSql.Final.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly BlogContext _context;

        public CommentsController(BlogContext context)
        {
            _context = context;    
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
