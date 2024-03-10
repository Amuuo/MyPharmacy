using System.Text.Json.Serialization;

namespace MyPharmacy.Data.Entities;

public sealed class Delivery
{
    public int Id { get; init; }

    public int WarehouseId { get; init; }

    public int PharmacyId { get; init; }
     
    public string DrugName { get; init; }

    public int UnitCount { get; init; }

    public decimal UnitPrice { get; init; }

    public decimal? TotalPrice { get; init; }

    public DateTime? DeliveryDate { get; init; }

    public DateTime? CreatedDate { get; init; }

    public DateTime? ModifiedDate { get; init; }

    public string? ModifiedBy { get; init; }

    [JsonIgnore]
    public Pharmacy? Pharmacy { get; init; }
    [JsonIgnore]
    public Warehouse? Warehouse { get; init; } 
}
