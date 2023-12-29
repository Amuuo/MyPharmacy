namespace MyPharmacy.Core.Utilities.Interfaces;

public interface IPagedResult<out T>
{
    int CurrentPage { get; }
    int TotalPages { get; }
    int PageSize { get; }
    int TotalCount { get; }
    IAsyncEnumerable<T> Data { get; }
}