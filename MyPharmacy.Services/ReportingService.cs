using Microsoft.Extensions.Logging;
using MyPharmacy.Core.Helpers;
using MyPharmacy.Core.Utilities.Interfaces;
using MyPharmacy.Data;
using MyPharmacy.Data.Models;
using MyPharmacy.Services.Interfaces;

namespace MyPharmacy.Services; 

/// <summary>
/// Service class for generating reports.
/// </summary>
public class ReportingService(
    ILogger<IReportService> logger, 
    IPharmacyDbContext dbContext) : IReportService 
{
    private readonly ILogger<IReportService> _logger = logger;
    private readonly IPharmacyDbContext _dbContext = dbContext;

    /// <summary>
    /// Retrieves the warehouse profit data asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the warehouse profit data.</returns>
    public Task<IServiceResult<IAsyncEnumerable<VwWarehouseProfit>>> GetWarehouseProfitAsync() 
    {
        _logger.LogDebug("Fetching warehouse profits...");

        var warehouseProfits = _dbContext.VwWarehouseProfits.AsAsyncEnumerable();

        _logger.LogDebug("Successfully fetched warehouse profits.");

        return Task.FromResult(ServiceHelper.BuildSuccessServiceResult(warehouseProfits));

    }

    /// <summary>
    /// Retrieves the delivery detail data asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the delivery detail data.</returns>
    public Task<IServiceResult<IAsyncEnumerable<VwDeliveryDetail>>> GetDeliveryDetailAsync() 
    {
        _logger.LogDebug("Fetching delivery details...");

        var deliveryDetails = _dbContext.VwDeliveryDetails.AsAsyncEnumerable();

        _logger.LogDebug("Successfully fetched delivery details.");

        return Task.FromResult(ServiceHelper.BuildSuccessServiceResult(deliveryDetails));
    }

    /// <summary>
    /// Retrieves the pharmacist sales summary data asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the pharmacist sales summary data.</returns>
    public Task<IServiceResult<IAsyncEnumerable<VwPharmacistSalesSummary>>> GetPharmacistSalesSummaryAsync() 
    {
        _logger.LogDebug("Fetching pharmacist sales summary...");

        var pharmacistSalesSummary = _dbContext.VwPharmacistSalesSummaries.AsAsyncEnumerable();

        _logger.LogDebug("Successfully fetched pharmacist sales summary.");

        return Task.FromResult(ServiceHelper.BuildSuccessServiceResult(pharmacistSalesSummary));
    }
}

