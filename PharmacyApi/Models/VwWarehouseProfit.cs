namespace PharmacyApi.Models;

public partial class VwWarehouseProfit
{
    public string Warehouse { get; set; } = null!;

    public decimal? TotalDeliveryRevenue { get; set; }

    public int? TotalUnitCount { get; set; }

    public decimal? AverageProfitPerUnit { get; set; }
}
