using Microsoft.Extensions.Logging;
using MyPharmacy.Core.Helpers;
using MyPharmacy.Core.Utilities.Interfaces;
using MyPharmacy.Data;
using MyPharmacy.Data.Entities;
using MyPharmacy.Services.Interfaces;

namespace MyPharmacy.Services; 

///<inheritdoc/>
public class ReportingService(
    ILogger<IReportingService> logger, 
    IPharmacyDbContext dbContext) : IReportingService 
{
 
    ///<inheritdoc/>
    public Task<IServiceResult<IAsyncEnumerable<VwWarehouseProfit>>> GetWarehouseProfitAsync() 
    {
        var warehouseProfits = dbContext.VwWarehouseProfits.AsAsyncEnumerable();

        return Task.FromResult(ServiceHelper.BuildSuccessServiceResult(warehouseProfits));

    }


    ///<inheritdoc/>
    public Task<IServiceResult<IAsyncEnumerable<VwDeliveryDetail>>> GetDeliveryDetailAsync() 
    {
        var deliveryDetails = dbContext.VwDeliveryDetails.AsAsyncEnumerable();

        return Task.FromResult(ServiceHelper.BuildSuccessServiceResult(deliveryDetails));
    }


    ///<inheritdoc/>
    public Task<IServiceResult<IAsyncEnumerable<VwPharmacistSalesSummary>>> GetPharmacistSalesSummaryAsync() 
    {
        var pharmacistSalesSummary = dbContext.VwPharmacistSalesSummaries.AsAsyncEnumerable();

        return Task.FromResult(ServiceHelper.BuildSuccessServiceResult(pharmacistSalesSummary));
    }
}

