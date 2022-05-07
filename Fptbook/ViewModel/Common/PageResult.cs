namespace Fptbook.ViewModel.common
{
    public class PageResult<T> : PagedResultBase
    {
        public List<T> Items { get; set; }
    }
}
