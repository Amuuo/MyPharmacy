using MyPharmacy.Core.Utilities.Interfaces;
using MyPharmacy.Data.Entities;

namespace MyPharmacy.Services.Interfaces;

/// <summary>
/// Service class for generating reports.
/// </summary>
public interface IReportingService
{
    /// <summary>
    /// Retrieves the warehouse profit data asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the warehouse profit data.</returns>
    Task<IServiceResult<IAsyncEnumerable<VwWarehouseProfit>>> GetWarehouseProfitAsync();
    
    /// <summary>
    /// Retrieves the delivery detail data asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the delivery detail data.</returns>
    Task<IServiceResult<IAsyncEnumerable<VwDeliveryDetail>>> GetDeliveryDetailAsync();

    /// <summary>
    /// Retrieves the pharmacist sales summary data asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the pharmacist sales summary data.</returns>
    Task<IServiceResult<IAsyncEnumerable<VwPharmacistSalesSummary>>> GetPharmacistSalesSummaryAsync();
}
