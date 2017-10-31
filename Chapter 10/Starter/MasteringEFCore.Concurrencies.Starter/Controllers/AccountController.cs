using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.Concurrencies.Starter.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MasteringEFCore.Concurrencies.Starter.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View(new RegistrationViewModel());
        }
    }
}