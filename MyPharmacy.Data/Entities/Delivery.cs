using System.Text.Json.Serialization;

namespace MyPharmacy.Data.Entities;

public class Delivery
{
    public int Id { get; set; }

    public int WarehouseId { get; set; }

    public int PharmacyId { get; set; }
     
    public string DrugName { get; set; } = null!;

    public int UnitCount { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal? TotalPrice { get; set; }

    public DateTime DeliveryDate { get; set; }

    [JsonIgnore]
    public virtual Pharmacy? Pharmacy { get; set; }
    [JsonIgnore]
    public virtual Warehouse? Warehouse { get; set; } 
}
