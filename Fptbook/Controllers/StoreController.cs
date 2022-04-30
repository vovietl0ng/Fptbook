using Fptbook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Fptbook.Controllers
{
    public class StoreController : Controller
    {
        private readonly ILogger<StoreController> _logger;

 

        public IActionResult Book()
        {
            return View();
        }

        public IActionResult Category()
        {
            return View();
        }

        public IActionResult Record()
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