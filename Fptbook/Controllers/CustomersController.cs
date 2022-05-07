using Fptbook.Models;
using Fptbook.Models.EF;
using Fptbook.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Controllers;

namespace Fptbook.Controllers
{
    [Authorize(Roles = "customer")]
    public class CustomersController : BaseController
    {
        private readonly FptDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CustomersController(FptDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        [HttpGet]
        public async Task<IActionResult> BuyBook()
        {

            return View();
        }


    }
}