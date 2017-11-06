using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MasteringEFCore.Transactions.Final.Data;
using MasteringEFCore.Transactions.Final.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data.SqlClient;
using MasteringEFCore.Transactions.Final.Extensions;

namespace MasteringEFCore.Transactions.Final.Controllers
{
    //[Authorize]
    [Route("Admin/Blogs")]
    public class BlogsController : Controller
    {
        private readonly BlogContext _context;

        public BlogsController(BlogContext context)
        {
            _context = context;    
        }

        // GET: Blogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Blogs.FromSql("Select * from dbo.Blog").ToListAsync());
            //return View(await _context.Blogs.FromSql("Select [Id],[Title],[Subtitle],[Description],[Url] from dbo.Blog").ToListAsync());
        }

        [Route("LatestBlogs")]
        public async Task<IActionResult> LatestBlogs()
        {
            //return View("Index", await _context.Blogs.FromSql("EXEC [dbo].[GetLatestBlogs]").ToListAsync());
            var comparisonDateTime = DateTime.Now.AddMonths(-3);
            //var results = await _context.Blogs
            //    .FromSql("Select * from dbo.Blog")
            //    .Where(x => x.CreatedAt >= comparisonDateTime)
            //    .OrderByDescending(x => x.Id)
            //    .Include(x => x.Posts)
            //    .ToListAsync();
            var results = await _context.Database
                .ExecuteSqlQueryAsync(@"select b.Title as BlogTitle, p.* from
                                        Post p join Blog b
                                        on b.Id = p.BlogId");
            while (results.DbDataReader.Read())
            {
                Console.Write($"Blog Title: '{results.DbDataReader["BlogTitle"]}', Post Title: '{results.DbDataReader["Title"]}'");
            }
            return View("Index", results);
        }

        // GET: Blogs/Details/5
        [Route("{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.FromSql("Select * from dbo.Blog WHERE Id = {0}", id).FirstOrDefaultAsync();
            //var blog = await _context.Blogs.FromSql("Select * from dbo.Blog WHERE Id = @id", new SqlParameter("id", id)).FirstOrDefaultAsync();
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        [Route("GetBlogByTitle/{keyword}")]
        public async Task<IActionResult> GetBlogByTitle(string keyword)
        {
            if (keyword == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.FromSql("Select * from dbo.Blog WHERE Title like '%' + @keyword + '%'",
                new SqlParameter("keyword", keyword)).FirstOrDefaultAsync();
            if (blog == null)
            {
                return NotFound();
            }

            return View("Details", blog);
        }

        [Route("BlogsByCategory/{categoryId:int}")]
        public async Task<IActionResult> BlogsByCategory(int categoryId)
        {
            return View("Index", await _context.Blogs.
                FromSql("EXEC [dbo].[GetBlogsByCategory] @categoryId = @Id",
                new SqlParameter("Id", categoryId)).ToListAsync());
            //return View("Index", await _context.Blogs.FromSql("EXEC [dbo].[GetBlogsByCategory] @categoryId = {0}", categoryId).ToListAsync());
        }

        // GET: Blogs/Create
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(blog);
        }

        // GET: Blogs/Edit/5
        [Route("Edit/{id:int?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.SingleOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Edit/{id:int?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Url")] Blog blog)
        {
            if (id != blog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.Id))
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
            return View(blog);
        }

        // GET: Blogs/Delete/5
        [Route("Delete/{id:int?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .SingleOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [Route("Delete/{id:int?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blog = await _context.Blogs.SingleOrDefaultAsync(m => m.Id == id);
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.Id == id);
        }
    }
}
