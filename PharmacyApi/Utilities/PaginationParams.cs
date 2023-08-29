namespace PharmacyApi.Utilities
{
    public class PaginationParams
    {
        public int     PageNumber    { get; set; } = 1;
        public int     PageSize      { get; set; } = 10;
        public string  SortColumn    { get; set; } = "Id";  
        public string  SortDirection { get; set; } = "ASC"; 
        public string? SearchQuery   { get; set; }
        public string? Filter        { get; set; }
    }
}
