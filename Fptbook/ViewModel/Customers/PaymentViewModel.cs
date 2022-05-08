namespace Fptbook.ViewModel.Customers
{
    public class PaymentViewModel
    {
        public int OrderId { get; set; }
        public int StoreId { get; set; }
        public string RecipientName { get; set; }
        public string RecipientAddress { get; set; }
        public string RecipientPhoneNumber { get; set; }
        public double TotalPrice { get; set; }
    }
}
