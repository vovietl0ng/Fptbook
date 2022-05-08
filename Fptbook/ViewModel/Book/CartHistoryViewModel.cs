using Fptbook.ViewModel.Customers;

namespace Fptbook.ViewModel.Book
{
    public class CartHistoryViewModel
    {
        public string RecipientName { get; set; }
        public string RecipientAddress { get; set; }
        public string RecipientPhoneNumber { get; set; }
        public List<ViewCartItemViewModel> ListItem { get; set; }
    }
}
