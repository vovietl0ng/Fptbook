using Fptbook.Models.EF;
using Fptbook.Models.Entity;
using Fptbook.ViewModel.Book;
using Fptbook.ViewModel.Category;
using Fptbook.ViewModel.common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Controllers;

namespace Fptbook.Controllers
{
    [Authorize(Roles ="store")]
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
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);
            var store = await _context.Stores.FirstOrDefaultAsync(x => x.UserId == user.Id);
            var query = await _context.Books.Where(x => x.StoreId == store.Id).ToListAsync();
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
                    Pages = x.Pages,
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
        public async Task<IActionResult> CreateBook(CreateBookRequest request)
        {
            var bookExist = await _context.Books.AnyAsync(x => x.Name == request.Name); 
            if (bookExist)
            {
                throw new Exception("Book is exist");
            }
            var user = _context.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            var store = await _context.Stores.FirstOrDefaultAsync(x => x.UserId == user.Id);
            var book = new Book()
            {
                Name = request.Name,
                Price = request.Price,
                Quanlity = request.Quanlity,
                Description = request.Description,
                Author = request.Author,
                ISBN = request.ISBN,
                CategoryId = request.CategoryId,
                DateCreated = DateTime.Now,
                StoreId = store.Id,
                ImagePath = request.ImagePath,
                Pages = request.Pages,
            };
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            TempData["result"] = "Create successfull";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBook(int id)
        {
            var categories = _context.Categories.ToList();
            ViewBag.Categories = categories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            });
            var book = await _context.Books.FindAsync(id);
            var bookUpdateRequest = new UpdateBookRequest()
            {
                Id = id,
                Name = book.Name,
                Price = book.Price,
                Quanlity = book.Quanlity,
                Description = book.Description,
                Author = book.Author,
                ISBN = book.ISBN,
                CategoryId = book.CategoryId,
                DateCreated = DateTime.Now,
                ImagePath = book.ImagePath,
                Pages= book.Pages,
            };
            return View(bookUpdateRequest);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBook(UpdateBookRequest request)
        {
            var bookExist = await _context.Books.AnyAsync(x => x.Name == request.Name && x.Id != request.Id);
            if (bookExist)
            {
                throw new Exception("Book is exist");
            }
            var book = await _context.Books.FindAsync(request.Id);
            book.Name = request.Name;
            book.Price = request.Price;
            book.Description = request.Description;
            book.Author = request.Author;
            book.ISBN = request.ISBN;
            book.CategoryId = request.CategoryId;  
            book.DateCreated = DateTime.Now;    
            book.Pages = request.Pages;
            book.Quanlity = request.Quanlity;
            await _context.SaveChangesAsync();
            TempData["result"] = "Update successfull";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> DetailBook(int id)
        {

            var book = await _context.Books.FindAsync(id);
            var category = await _context.Categories.FindAsync(book.CategoryId);
            var store = await _context.Stores.FindAsync(book.StoreId);
            var detailBook = new DetailBookViewModel()
            {
                Id = id,
                Name = book.Name,
                Price = book.Price,
                Quanlity = book.Quanlity,
                Description = book.Description,
                Author = book.Author,
                ISBN = book.ISBN,
                DateCreated = DateTime.Now,
                ImagePath = book.ImagePath,
                Pages = book.Pages,
                CategoryName = category.Name,
                StoreName = store.Name,
            };
            return View(detailBook);
        }
    }
}
