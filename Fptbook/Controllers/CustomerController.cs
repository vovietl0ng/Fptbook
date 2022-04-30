using Fptbook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Fptbook.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;

        //public CustomerController(ILogger<CustomerController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult CheckoutScreen()
        {
            return View();
        }

        public IActionResult HelpScreen()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}