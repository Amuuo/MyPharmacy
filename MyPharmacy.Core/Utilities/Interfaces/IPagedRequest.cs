namespace MyPharmacy.Core.Utilities.Interfaces;

public interface IPagedRequest
{
    int Page { get; set; }
    int Take   { get; set; }
}