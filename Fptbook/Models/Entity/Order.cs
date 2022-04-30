namespace Fptbook.Models.Entity
{
    public class Order
    {
        public Guid UserId { get; set; }
        public AppUser AppUser { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalPrice { get; set; }

    }
}
