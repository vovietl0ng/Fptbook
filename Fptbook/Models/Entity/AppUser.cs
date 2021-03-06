using Microsoft.AspNetCore.Identity;

namespace Fptbook.Models.Entity
{
    public class AppUser : IdentityUser<Guid> 
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public Store Store { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<CartItem> CartItems { get; set; }


    }
}
