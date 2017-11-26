using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MasteringEFCore.MultiTenancy.Starter.Data;
using MasteringEFCore.MultiTenancy.Starter.Models;
using Microsoft.AspNetCore.Authorization;
using MasteringEFCore.MultiTenancy.Starter.ViewModels;
using MasteringEFCore.MultiTenancy.Starter.Helpers;

namespace MasteringEFCore.MultiTenancy.Starter.Controllers
{
    //[Authorize]
    public class UsersController : Controller
    {
        private readonly BlogContext _context;

        public UsersController(BlogContext context)
        {
            _context = context;    
        }

        public async Task<IActionResult> IsUsernameAvailable(string username)
        {
            var usernameAvailable =
                await _context.Users.AnyAsync(x => x.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            return Json(data: !usernameAvailable );
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DisplayName,Username,PasswordHash,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DisplayName,Username,PasswordHash,Email")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> Register(RegistrationViewModel registrationViewModel)
        {
            var user = new User
            {
                Username = registrationViewModel.Username,
                Email = registrationViewModel.Email,
                PasswordHash = Cryptography.Instance.HashPassword(registrationViewModel.Password)
            };


            if (TryValidateModel(user))
            {
                await _context.Users.AddAsync(user);
                ViewBag.Message = "User created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Error occurred while validating the user";
            }

            return View(user);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
