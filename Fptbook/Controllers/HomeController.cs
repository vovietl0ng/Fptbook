using Fptbook.Models;
using Fptbook.Models.EF;
using Fptbook.Models.Entity;
using Fptbook.ViewModel.Book;
using Fptbook.ViewModel.common;
using Fptbook.ViewModel.Home;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApp.Controllers;
using System.Linq;

namespace Fptbook.Controllers
{
    public class HomeController : Controller
    {
        private readonly FptDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(FptDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 5)
        {
            var homeVMs = new List<HomeViewModel>();
            var stores = await _context.Stores.ToListAsync();
            foreach (var store in stores)
            {
                var homeVM = new HomeViewModel();
                homeVM.StoreName = store.Name;
                homeVM.StoreId = store.Id;
                var query = await _context.Books.Where(x => x.StoreId == store.Id).ToListAsync();         
                var books = query.AsQueryable();
                books = books.OrderBy(x => x.Pages);
                int totalRow = books.Count();
                var data = books.Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new BookViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        DateCreated = x.DateCreated,
                        Description = x.Description,
                        Author = x.Author,
                        Price = x.Price,
                        ISBN = x.ISBN,
                        ImagePath = x.ImagePath,
                        Pages = x.Pages,
                    }).ToList();


                var pagedResult = new PageResult<BookViewModel>()
                {
                    TotalRecords = totalRow,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Items = data
                };
                homeVM.BookList = pagedResult;
                homeVMs.Add(homeVM);
            }
            return View(homeVMs);
        }

        

    }
}