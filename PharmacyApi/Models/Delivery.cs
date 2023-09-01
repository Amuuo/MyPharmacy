using System;
using System.Collections.Generic;

namespace PharmacyApi.Models;

public partial class Delivery
{
    public int Id { get; set; }

    public int WarehouseId { get; set; }

    public int PharmacyId { get; set; }

    public string DrugName { get; set; } = null!;

    public int UnitCount { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal? TotalPrice { get; set; }

    public DateTime DeliveryDate { get; set; }

    public virtual Pharmacy Pharmacy { get; set; } = null!;

    public virtual Warehouse Warehouse { get; set; } = null!;
}
