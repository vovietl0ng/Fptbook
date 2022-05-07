using Fptbook.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Controllers;

namespace Fptbook.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
      

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var usser = User.Identity.Name;
            return View();
        }

        //public async Task<IActionResult> LoginAsync()
        //{
        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> Login(LoginRequest request)
        //{

        //    if (!ModelState.IsValid)
        //        return View(ModelState);

        //    var token = await _user.Authenticate(request);

        //    var userPrincipal = this.ValidateToken(token);
        //    var authProperties = new AuthenticationProperties
        //    {
        //        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
        //        IsPersistent = true
        //    };
        //    HttpContext.Session.SetString("Token", token);
        //    await HttpContext.SignInAsync(
        //                CookieAuthenticationDefaults.AuthenticationScheme,
        //                userPrincipal,
        //                authProperties);

        //    return RedirectToAction("Index", "Home");
        //}

        //[HttpPost]
        //public async Task<IActionResult> Logout()
        //{
        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    HttpContext.Session.Remove("Token");
        //    return RedirectToAction("Login", "User");
        //}

    }
}