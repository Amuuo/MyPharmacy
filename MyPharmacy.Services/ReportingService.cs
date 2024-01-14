using Microsoft.Extensions.Logging;
using MyPharmacy.Core.Helpers;
using MyPharmacy.Core.Utilities.Interfaces;
using MyPharmacy.Data;
using MyPharmacy.Data.Entities;
using MyPharmacy.Services.Interfaces;

namespace MyPharmacy.Services; 

/// <summary>
/// Service class for generating reports.
/// </summary>
public class ReportingService(
    ILogger<IReportService> logger, 
    IPharmacyDbContext dbContext) : IReportService 
{
    /// <summary>
    /// Retrieves the warehouse profit data asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the warehouse profit data.</returns>
    public Task<IServiceResult<IAsyncEnumerable<VwWarehouseProfit>>> GetWarehouseProfitAsync() 
    {
        logger.LogDebug("Fetching warehouse profits...");

        var warehouseProfits = dbContext.VwWarehouseProfits.AsAsyncEnumerable();

        logger.LogDebug("Successfully fetched warehouse profits.");

        return Task.FromResult(ServiceHelper.BuildSuccessServiceResult(warehouseProfits));

    }

    /// <summary>
    /// Retrieves the delivery detail data asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the delivery detail data.</returns>
    public Task<IServiceResult<IAsyncEnumerable<VwDeliveryDetail>>> GetDeliveryDetailAsync() 
    {
        logger.LogDebug("Fetching delivery details...");

        var deliveryDetails = dbContext.VwDeliveryDetails.AsAsyncEnumerable();

        logger.LogDebug("Successfully fetched delivery details.");

        return Task.FromResult(ServiceHelper.BuildSuccessServiceResult(deliveryDetails));
    }

    /// <summary>
    /// Retrieves the pharmacist sales summary data asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the pharmacist sales summary data.</returns>
    public Task<IServiceResult<IAsyncEnumerable<VwPharmacistSalesSummary>>> GetPharmacistSalesSummaryAsync() 
    {
        logger.LogDebug("Fetching pharmacist sales summary...");

        var pharmacistSalesSummary = dbContext.VwPharmacistSalesSummaries.AsAsyncEnumerable();

        logger.LogDebug("Successfully fetched pharmacist sales summary.");

        return Task.FromResult(ServiceHelper.BuildSuccessServiceResult(pharmacistSalesSummary));
    }
}

