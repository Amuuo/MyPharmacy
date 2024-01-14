using MyPharmacy.Core.Utilities.Interfaces;
using MyPharmacy.Data.Entities;

namespace MyPharmacy.Services.Interfaces;

public interface IReportService
{
    Task<IServiceResult<IAsyncEnumerable<VwWarehouseProfit>>> GetWarehouseProfitAsync();
    Task<IServiceResult<IAsyncEnumerable<VwDeliveryDetail>>> GetDeliveryDetailAsync();
    Task<IServiceResult<IAsyncEnumerable<VwPharmacistSalesSummary>>> GetPharmacistSalesSummaryAsync();
}
