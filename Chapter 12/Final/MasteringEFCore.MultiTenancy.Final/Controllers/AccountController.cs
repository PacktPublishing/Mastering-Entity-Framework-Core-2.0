using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.MultiTenancy.Final.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using MasteringEFCore.MultiTenancy.Final.Repositories;
using MasteringEFCore.MultiTenancy.Final.Infrastructure.Queries.Users;
using MasteringEFCore.MultiTenancy.Final.Data;
using MasteringEFCore.MultiTenancy.Final.Helpers;

namespace MasteringEFCore.MultiTenancy.Final.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly BlogContext _context;
        public AccountController(BlogContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View(new RegistrationViewModel());
        }

        [AllowAnonymous]
        public IActionResult Login(string redirectUrl)
        {
            ViewBag.RedirectUrl = redirectUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var user = await _userRepository.GetSingleAsync(
                new GetUserByAuthenticationQuery(_context)
                {
                    Username = loginViewModel.Username,
                    PasswordHash = Cryptography.Instance.HashPassword(loginViewModel.Password)
                });

            if (user == null) RedirectToAction("AccessDenied", "Account");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginViewModel.Username)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
            return RedirectToAction("Index", "Home");
        }
        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}