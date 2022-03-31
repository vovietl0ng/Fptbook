namespace Fptbook.Models.Entity
{
    public class Book
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public int CategoryId { get; set; }
        public int Price { get; set; }
        public int Quanlity { get; set; }
        public string Description {get; set; }
    }
}
