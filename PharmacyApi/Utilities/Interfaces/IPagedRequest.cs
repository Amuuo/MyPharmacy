namespace PharmacyApi.Utilities.Interfaces
{
    public interface IPagedRequest
    {
        int PageNumber { get; set; }
        int PageSize   { get; set; }
    }
}
