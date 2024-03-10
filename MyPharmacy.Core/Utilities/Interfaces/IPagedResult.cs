namespace MyPharmacy.Core.Utilities.Interfaces;

public interface IPagedResult<out T>
{
    PagingInfo? PagingInfo { get; }
    int Pages { get; }
    int Total { get; }
    IEnumerable<T>? Data { get; }
}