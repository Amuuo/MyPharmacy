namespace MyPharmacy.Data.Models;

public partial class VwDeliveryDetail
{
    public int VwDeliveryDetailId { get; set; }
    
    public string WarehouseFrom { get; set; } = null!;

    public string PharmacyTo { get; set; } = null!;

    public string DrugName { get; set; } = null!;

    public int UnitCount { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal? TotalPrice { get; set; }

    public DateTime DeliveryDate { get; set; }
}
