using MyPharmacy.Core.Utilities.Interfaces;

namespace MyPharmacy.Core.Utilities;

public class PagingInfo : IPagedRequest
{
    public int Page { get; set; }
    public int Take { get; set; } = 10;
}