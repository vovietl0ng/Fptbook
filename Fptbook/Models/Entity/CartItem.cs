﻿namespace Fptbook.Models.Entity
{
    public class CartItem
    {
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public int Quantity { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public Guid UserId  { get; set; }
        public AppUser User { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }

    }
}
