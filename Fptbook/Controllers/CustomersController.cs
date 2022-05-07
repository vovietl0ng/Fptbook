using Fptbook.Models;
using Fptbook.Models.EF;
using Fptbook.Models.Entity;
using Fptbook.ViewModel.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> AddToCart(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.UserId == user.Id && x.Status == true);
            if (order == null)
            {
                var newOrder = new Order()
                {
                    UserId = user.Id,
                    Status = true
                };
                await _context.Orders.AddAsync(newOrder);
                await _context.SaveChangesAsync();
                var addToCart = new AddToCartViewModel()
                {
                    OrderId = newOrder.Id,
                };
                return View(addToCart);
            }

            var addToCartVM = new AddToCartViewModel()
            {
                OrderId = order.Id,
                BookId = id
            };
            return View(addToCartVM);


        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(AddToCartViewModel request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);
            var book = await _context.Books.FindAsync(request.BookId);
            var cartItem = new CartItem()
            {
                Quantity = request.Quantily,
                BookId = book.Id,
                UserId = user.Id,
                OrderId = request.OrderId,
                TotalPrice = book.Price * request.Quantily
            };
            await _context.CartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");


        }

        [HttpGet]
        public async Task<IActionResult> ViewCart()
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.UserId == user.Id && x.Status == true);
            var cartItems = await _context.CartItems.Where(x => x.UserId == user.Id && x.OrderId == order.Id).ToListAsync();
            var viewCartVMs = new List<ViewCartItemViewModel>();
            foreach (var cartItem in cartItems)
            {
                var book = await _context.Books.FindAsync(cartItem.BookId);
                var viewCartVM = new ViewCartItemViewModel()
                {
                    Id = cartItem.Id,
                    BookName = book.Name,
                    Quantity = cartItem.Quantity,
                    TotalPrice = cartItem.TotalPrice
                };
                viewCartVMs.Add(viewCartVM);
            }
            return View(viewCartVMs);
        }
    }
}