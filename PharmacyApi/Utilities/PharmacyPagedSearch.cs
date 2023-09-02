using PharmacyApi.Utilities.Interfaces;

namespace PharmacyApi.Utilities
{
    public class PharmacyPagedSearch : IPagedSearchCriteria<PharmacySearchCriteria>
    {
        public int     PageNumber    { get; set; } = 1; 
        public int     PageSize      { get; set; } = int.MaxValue;
        public string? SortColumn    { get; set; } = "Id";
        public string? SortDirection { get; set; } = "ASC";
        public PharmacySearchCriteria SearchCriteria { get; set; } = new();
    }
}
