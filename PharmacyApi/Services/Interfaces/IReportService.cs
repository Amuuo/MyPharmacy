using PharmacyApi.Models;
using PharmacyApi.Utilities.Interfaces;

namespace PharmacyApi.Services.Interfaces
{
    public interface IReportService
    {
        Task<IServiceResult<IAsyncEnumerable<VwWarehouseProfit>>> GetWarehouseProfitAsync();
        Task<IServiceResult<IAsyncEnumerable<VwDeliveryDetail>>> GetDeliveryDetailAsync();
        Task<IServiceResult<IAsyncEnumerable<VwPharmacistSalesSummary>>> GetPharmacistSalesSummaryAsync();
    }
}
