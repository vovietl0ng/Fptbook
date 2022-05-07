using Fptbook.ViewModel.common;

namespace Fptbook.ViewModel.Book
{
    public class GetBookPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
        public int? CategoryId { get; set; }
    }
}
