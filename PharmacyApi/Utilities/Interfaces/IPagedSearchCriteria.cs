namespace PharmacyApi.Utilities.Interfaces
{
    public interface IPagedSearchCriteria<T>
    {
        int     PageNumber     { get; set; }
        int     PageSize       { get; set; }
        string? SortColumn     { get; set; }
        string? SortDirection  { get; set; }
        T       SearchCriteria { get; set; }
    }
}
