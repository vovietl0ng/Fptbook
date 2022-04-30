namespace Fptbook.Models.Entity
{
    public class CartItem
    {
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }

    }
}
