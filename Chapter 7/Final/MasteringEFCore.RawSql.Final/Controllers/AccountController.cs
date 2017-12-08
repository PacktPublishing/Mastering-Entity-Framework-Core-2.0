using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.RawSql.Final.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MasteringEFCore.RawSql.Final.Controllers
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