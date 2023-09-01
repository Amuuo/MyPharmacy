namespace PharmacyApi.Utilities
{
    public class PharmacySearch
    {
        public int?    Id { get; set; }
        public string? SearchQuery { get; set; }
        public int     PageNumber { get; set; } = 1; 
        public int     PageSize { get; set; } = 10;
        public string? SortColumn { get; set; } = "Id";
        public string? SortDirection { get; set; } = "ASC";
    }

}
