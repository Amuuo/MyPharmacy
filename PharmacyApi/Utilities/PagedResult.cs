using PharmacyApi.Utilities.Interfaces;

namespace PharmacyApi.Utilities
{
    public class PagedResult<T> : IPagedResult<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IAsyncEnumerable<T> Data { get; set; }
    }

}
