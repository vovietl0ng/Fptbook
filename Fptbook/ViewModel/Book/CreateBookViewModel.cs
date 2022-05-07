namespace Fptbook.ViewModel.Book
{
    public class CreateBookViewModel
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
        public int CategoryId { get; set; }
        public int Pages { get; set; }
    }
}
