namespace Fptbook.Models.Entity
{
    public class Cart
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<Order> Orders { get; set; }
        public int TotalPrice { get; set; }
    }
}
