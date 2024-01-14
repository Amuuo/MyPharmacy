using MyPharmacy.Core.Utilities.Interfaces;

namespace MyPharmacy.Core.Utilities;

public class PagingInfo : IPagedRequest
{
    public int Page { get; set; } = 0;
    public int Take { get; set; } = int.MaxValue;
}