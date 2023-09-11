using PharmacyApi.Utilities.Interfaces;

namespace PharmacyApi.Utilities
{
    public class PagedRequest : IPagedRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = int.MaxValue;
    }
}
