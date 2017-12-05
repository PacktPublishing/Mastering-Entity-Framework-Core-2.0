using MasteringEFCore.Performance.Final.Data;
using MasteringEFCore.Performance.Final.Infrastructure.Queries.People;
using MasteringEFCore.Performance.Final.Repositories;
using MasteringEFCore.Performance.Final.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Performance.Final.ViewComponents
{
    public class AboutViewComponent : ViewComponent
    {
        private readonly BlogContext _context;
        private readonly IPersonRepository _repository;
        public AboutViewComponent(IPersonRepository repository,
            BlogContext context)
        {
            _repository = repository;
            _context = context;
        }

        public IViewComponentResult Invoke(int id)
        {
            //var user = _repository.GetSingle(
            //    new GetPersonByIdQuery(_context)
            //    {
            //        IncludeData = true,
            //        Id = id
            //    });
            //return View(user);

            var person = _context.People
                .AsNoTracking()
                .FirstOrDefault(item => item.Id.Equals(id));

            var aboutViewModel = _context.People
                .AsNoTracking()
                //.Where(item => item.PhoneNumber.Equals("9876543210"))
                .Where(item => item.FirstName.Equals(person.FirstName) 
                    && item.LastName.Equals(person.LastName))
                .Select(item => new AboutViewModel
                {
                    Name = item.FirstName,
                    Biography = item.Biography
                }).SingleOrDefault();

            return View(aboutViewModel);
        }
    }
}
