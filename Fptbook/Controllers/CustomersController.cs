using Fptbook.Models;
using Fptbook.Models.EF;
using Fptbook.Models.Entity;
using Fptbook.ViewModel.Customers;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
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
            var book = await _context.Books.FindAsync(id);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.UserId == user.Id && x.Status == true && x.StoreId == book.StoreId);
            if (order == null)
            {
                var newOrder = new Order()
                {
                    UserId = user.Id,
                    Status = true,
                    StoreId = book.StoreId,

                };
                await _context.Orders.AddAsync(newOrder);
                await _context.SaveChangesAsync();
                var addToCart = new AddToCartViewModel()
                {
                    OrderId = newOrder.Id,
                    BookId = id

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
        public async Task<IActionResult> ViewCart(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.UserId == user.Id && x.Status == true && x.StoreId == id);
            if (order != null)
            {
                ViewBag.OrderId = order.Id;
                ViewBag.StoreId = id;
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
                        TotalPrice = Math.Round(cartItem.TotalPrice, 3) 
                    };
                    viewCartVMs.Add(viewCartVM);
                }
                return View(viewCartVMs);

            }
            var viewCartVMNew = new List<ViewCartItemViewModel>();
            return View(viewCartVMNew);

        }
        [HttpGet]
        public async Task<IActionResult> ChangeQuantity(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            var changeQuantity = new ChangeQuantityViewModel()
            {
                Quantity = cartItem.Quantity,
                Id = cartItem.Id
            };
            return View(changeQuantity);
        }
        [HttpPost]
        public async Task<IActionResult> ChangeQuantity(ChangeQuantityViewModel request)
        {
            var cartItem = await _context.CartItems.FindAsync(request.Id);
            var order = await _context.Orders.FindAsync(cartItem.OrderId);
            var book = await _context.Books.FindAsync(cartItem.BookId);
            cartItem.Quantity = request.Quantity;
            cartItem.TotalPrice = book.Price * request.Quantity;
            await _context.SaveChangesAsync();

            return RedirectToAction("ViewCart", new {id =order.StoreId});
        }


        public async Task<IActionResult> RemoveItem(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            var order = await _context.Orders.FindAsync(cartItem.OrderId);


            return RedirectToAction("ViewCart", new { id = order.StoreId });
        }
        [HttpGet]
        public async Task<IActionResult> Payment(int orderId, int storeId)
        {
            var items = await _context.CartItems.Where(x => x.OrderId == orderId).ToListAsync();
            double totalPrice = 0;
            foreach (var item in items)
            {
                totalPrice += item.TotalPrice;
            }
            var payment = new PaymentViewModel()
            {
                TotalPrice = Math.Round(totalPrice, 3),
                OrderId = orderId,
                StoreId = storeId
            };
            return View(payment);
        }
        [HttpPost]
        public async Task<IActionResult> Payment(PaymentViewModel request)
        {
            var order = await _context.Orders.FindAsync(request.OrderId);
            order.Status = false;
            var cart = new Cart()
            {
                OrderId = request.OrderId,
                RecipientName = request.RecipientName,
                RecipientAddress = request.RecipientAddress,
                RecipientPhoneNumber = request.RecipientPhoneNumber,
                TotalPrice = request.TotalPrice,
            };
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
            
            var store = await _context.Stores.FindAsync(request.StoreId);
            var user = await _context.Users.FindAsync(store.UserId);
            
            string to = user.Email;
            string subject = "Notification";
            string body = "Someone bought your book, please check it out";


            var email = new MimeMessage();

            email.Sender = new MailboxAddress("Notification", "vovietlong123@gmail.com");
            email.From.Add(new MailboxAddress("Notification", "vovietlong123@gmail.com"));
            email.To.Add(new MailboxAddress(to, to));
            email.Subject = subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = body;
            email.Body = builder.ToMessageBody();
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                //kết nối máy chủ
                await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                // xác thực
                await smtp.AuthenticateAsync("vovietlong123@gmail.com", "Long1233211234");
                //gởi
                await smtp.SendAsync(email);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            smtp.Disconnect(true);
            return RedirectToAction("ViewCart", new { id = request.StoreId });
        }
        public IActionResult Helpscreen()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);
            var profile =  new ViewProfileViewModel()
            {
                FullName = user.FullName,
                Address = user.Address,
            };
            return View(profile);
        }

       
    }
}