namespace Fptbook.Models.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public AppUser AppUser { get; set; }
        public bool Status { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public Cart Cart { get; set; }


    }
}
