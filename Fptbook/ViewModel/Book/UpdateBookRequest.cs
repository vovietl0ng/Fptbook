namespace Fptbook.ViewModel.Book
{
    public class UpdateBookRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string ImagePath { get; set; }
        public int CategoryId { get; set; }
        public double Pages { get; set; }
        public int StoreId { get; set; }
    }
}
