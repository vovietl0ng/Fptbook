using Microsoft.AspNetCore.Mvc;

namespace Fptbook.Controllers
{
    public class BookController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
