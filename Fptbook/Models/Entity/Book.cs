namespace Fptbook.Models.Entity
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quanlity { get; set; }
        public string Description {get; set; }
        public DateTime DateCreated { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public CartItem CartItem { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
