using MyPharmacy.Core.Utilities.Interfaces;

namespace MyPharmacy.Core.Utilities;

public class PagedResult<T> : IPagedResult<T>
{
    public PagingInfo? PagingInfo { get; init; }
    public int Pages { get; init; }
    public int Total { get; init; }
    public IEnumerable<T>? Data { get; init; }
}