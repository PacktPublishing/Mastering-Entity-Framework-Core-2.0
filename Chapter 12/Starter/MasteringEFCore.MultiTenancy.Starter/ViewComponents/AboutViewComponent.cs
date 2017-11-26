using MasteringEFCore.MultiTenancy.Starter.Data;
using MasteringEFCore.MultiTenancy.Starter.Infrastructure.Queries.People;
using MasteringEFCore.MultiTenancy.Starter.Repositories;
using MasteringEFCore.MultiTenancy.Starter.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.MultiTenancy.Starter.ViewComponents
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

            var aboutViewModel = _context.People
                //.Where(item => item.PhoneNumber.Equals("9876543210"))
                .Where(item => item.Id.Equals(id))
                .Select(item => new AboutViewModel
                {
                    Name = item.FirstName,
                    Biography = item.Biography
                }).SingleOrDefault();

            return View(aboutViewModel);
        }
    }
}
