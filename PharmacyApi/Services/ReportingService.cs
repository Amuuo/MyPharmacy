using PharmacyApi.Data;
using PharmacyApi.Models;
using PharmacyApi.Services.Interfaces;
using PharmacyApi.Utilities.Helpers;
using PharmacyApi.Utilities.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace PharmacyApi.Services; 

public class ReportingService : IReportService 
{
    private readonly ILogger<IReportService> _logger;
    private readonly IPharmacyDbContext _dbContext;

    public ReportingService(ILogger<IReportService> logger, IPharmacyDbContext dbContext) 
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<IServiceResult<IAsyncEnumerable<VwWarehouseProfit>>> GetWarehouseProfitAsync() 
    {
        try 
        {
            _logger.LogDebug("Fetching warehouse profits...");

            var warehouseProfits = _dbContext.VwWarehouseProfits.AsAsyncEnumerable();

            _logger.LogDebug("Successfully fetched warehouse profits.");

            return ServiceHelper.BuildSuccessServiceResult(warehouseProfits);
        } 
        catch (Exception ex) 
        {
            _logger.LogError(ex, "Error occurred while fetching warehouse profits.");
            return ServiceHelper.BuildErrorServiceResult<IAsyncEnumerable<VwWarehouseProfit>>(ex, "fetching warehouse profits");
        }
    }

    public async Task<IServiceResult<IAsyncEnumerable<VwDeliveryDetail>>> GetDeliveryDetailAsync() 
    {
        try 
        {
            _logger.LogDebug("Fetching delivery details...");

            var deliveryDetails = _dbContext.VwDeliveryDetails.AsAsyncEnumerable();

            _logger.LogDebug("Successfully fetched delivery details.");

            return ServiceHelper.BuildSuccessServiceResult(deliveryDetails);
        } 
        catch (Exception ex) 
        {
            _logger.LogError(ex, "Error occurred while fetching delivery details.");
            return ServiceHelper.BuildErrorServiceResult<IAsyncEnumerable<VwDeliveryDetail>>(ex, "fetching delivery details");
        }
    }

    public async Task<IServiceResult<IAsyncEnumerable<VwPharmacistSalesSummary>>> GetPharmacistSalesSummaryAsync() 
    {
        try 
        {
            _logger.LogDebug("Fetching pharmacist sales summary...");

            var pharmacistSalesSummary = _dbContext.VwPharmacistSalesSummaries.AsAsyncEnumerable();

            _logger.LogDebug("Successfully fetched pharmacist sales summary.");

            return ServiceHelper.BuildSuccessServiceResult(pharmacistSalesSummary);
        } 
        catch (Exception ex) 
        {
            _logger.LogError(ex, "Error occurred while fetching pharmacist sales summary.");
            return ServiceHelper.BuildErrorServiceResult<IAsyncEnumerable<VwPharmacistSalesSummary>>(ex, "fetching pharmacist sales summary");
        }
    }
}

