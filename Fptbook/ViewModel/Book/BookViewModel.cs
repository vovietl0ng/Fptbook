namespace Fptbook.ViewModel.Book
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quanlity { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string ImagePath { get; set; }
    }
}
