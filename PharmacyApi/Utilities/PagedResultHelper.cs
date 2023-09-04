using PharmacyApi.Utilities.Interfaces;

namespace PharmacyApi.Utilities
{
    public static class PagedResultHelper
    {
        public static async Task<IPagedResult<T>> BuildPagedResultAsync<T>(
            IAsyncEnumerable<T> data,
            int currentPage,
            int pageSize,
            int totalCount
        )
        {
            return new PagedResult<T>
            {
                CurrentPage = currentPage,
                PageSize    = pageSize,
                TotalCount  = totalCount,
                TotalPages  = (int)Math.Ceiling(totalCount / (double)pageSize),
                Data        = data
            };
        }
    }
}
