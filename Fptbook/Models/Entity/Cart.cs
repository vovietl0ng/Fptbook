namespace Fptbook.Models.Entity
{
    public class Cart
    {
        public int Id { get; set; }
       
        public ICollection<CartItem> CartItems { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public string RecipientName { get; set; }
        public string RecipientAddress { get; set; }
        public string RecipientPhoneNumber { get; set; }
        public int TotalPrice { get; set; }
    }
}
