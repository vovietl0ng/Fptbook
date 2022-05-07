namespace Fptbook.ViewModel.Book
{
    public class DetailBookViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string ImagePath { get; set; }
        public string CategoryName { get; set; }
        public double Pages { get; set; }
        public string StoreName { get; set; }
    }
}
