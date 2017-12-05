using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MasteringEFCore.Performance.Final.Data;
using MasteringEFCore.Performance.Final.Models;
using Microsoft.AspNetCore.Authorization;
using MasteringEFCore.Performance.Final.ViewModels;

namespace MasteringEFCore.Performance.Final.Controllers
{
    //[Authorize(Roles = "Administrators")]
    public class PeopleController : Controller
    {
        private readonly BlogContext _context;

        public PeopleController(BlogContext context)
        {
            _context = context;
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            var people = await _context.People.Take(100)
                .ToListAsync();
            var peopleViewModel = new List<PersonViewModel>();
            people.ForEach(item =>
            {
                var comment = item.Comments;
                peopleViewModel.Add(new PersonViewModel
                {
                    Person = item,
                    NoOfComments = item.Comments != null ? item.Comments.Count : 0
                });
            });
            return View(peopleViewModel);
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,NickName,Url,Biography,ImageUrl")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,NickName,Url,Biography,ImageUrl")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            var personToUpdate = await _context.People
                .SingleOrDefaultAsync(item => item.Id.Equals(person.Id));
            if (personToUpdate == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    personToUpdate.Biography = person.Biography;
                    personToUpdate.Comments = person.Comments;
                    personToUpdate.FirstName = person.FirstName;
                    personToUpdate.ImageUrl = person.ImageUrl;
                    personToUpdate.LastName = person.LastName;
                    personToUpdate.ModifiedAt = DateTime.Now;
                    personToUpdate.NickName = person.NickName;
                    personToUpdate.PhoneNumber = person.PhoneNumber;
                    personToUpdate.Url = person.Url;
                    personToUpdate.User = person.User;
                    _context.Update(personToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
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
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var person = await _context.People.SingleOrDefaultAsync(m => m.Id == id);
            //_context.People.Remove(person);
            Person person = new Person() { Id = id };
            _context.Entry(person).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PersonExists(int id)
        {
            return _context.People
                .AsNoTracking()
                .Any(e => e.Id == id);
        }
    }
}
