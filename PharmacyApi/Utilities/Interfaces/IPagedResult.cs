namespace PharmacyApi.Utilities.Interfaces
{
    public interface IPagedResult<T>
    {
        int CurrentPage { get; }
        int TotalPages { get; }
        int PageSize { get; }
        int TotalCount { get; }
        IAsyncEnumerable<T> Data { get; }
    }
}
