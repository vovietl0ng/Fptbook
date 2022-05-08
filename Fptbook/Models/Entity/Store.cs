namespace Fptbook.Models.Entity
{
    public class Store
    {
        public int Id { get; set; }
        public String Address { get; set; }
        public string Name { get; set; }
        public string Slogan { get; set; }
        public Guid UserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<Book> Books { get; set; }
        public ICollection<Order> Orders { get; set; }

    }
}
