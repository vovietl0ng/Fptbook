using Fptbook.Models.EF;
using Fptbook.Models.Entity;
using Fptbook.ViewModel.Book;
using Fptbook.ViewModel.Category;
using Fptbook.ViewModel.common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using WebApp.Controllers;

namespace Fptbook.Controllers
{
    [Authorize(Roles ="customer")]
    public class BooksController : BaseController
    {
        private readonly FptDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public BooksController(FptDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index(string keyword, int pageIndex = 1, int pageSize = 1)
        {
            var query = _context.Books.ToList();
            var books = query.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                books = books.Where(x => x.Name.Contains(keyword));
            }
            

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
                    Quanlity = x.Quanlity,
                    ISBN = x.ISBN,
                    ImagePath = x.ImagePath,
                }).ToList();


            var pagedResult = new PageResult<BookViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = data
            };

           
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(pagedResult);
        }
        [HttpGet]
        public IActionResult CreateBook()
        {
            var categories = _context.Categories.ToList();
            var store = _context.Stores.ToList();
            ViewBag.Categories = categories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            });
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(CreateBookViewModel request)
        {

            var userName = User.Identity.Name;
            var user = _context.Users.FirstOrDefault(x => x.UserName == userName);
            //var book = new Book()
            //{
            //    Name = request.Name,
            //    Price = request.Price,
            //    Quanlyti = request.Quanlyti,
            //    Description = request.Description,
            //    Author = request.Author,
            //    ISBN = request.ISBN,
            //    Category = request.Category,
            //    DateCreated = DateTime.Now
            //}
            return View();
        }
    }
}
