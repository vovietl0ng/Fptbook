using Fptbook.ViewModel.Book;
using Fptbook.ViewModel.common;

namespace Fptbook.ViewModel.Home
{
    public class HomeViewModel
    {
        public string StoreName { get; set; }
        public PageResult<BookViewModel> BookList { get; set; }
    }
}
