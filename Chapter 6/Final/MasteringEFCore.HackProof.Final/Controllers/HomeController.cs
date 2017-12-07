using MasteringEFCore.HackProof.Final.Models;
using MasteringEFCore.HackProof.Final.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MasteringEFCore.HackProof.Final.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Register()
        {
            return View(new RegistrationViewModel());
        }
    }
}