
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPharmacy.Core.Helpers;
using MyPharmacy.Core.Utilities;
using MyPharmacy.Core.Utilities.Interfaces;
using MyPharmacy.Data;
using MyPharmacy.Data.Entities;
using MyPharmacy.Services.Interfaces;

namespace MyPharmacy.Services;

public class DeliveryService(
    ILogger<DeliveryService> logger, 
    IPharmacyDbContext dbContext) : IDeliveryService
{
    /// <summary>
    /// Asynchronously retrieves a paginated list of deliveries.
    /// </summary>
    /// <param name="pageNumber">The page number for pagination.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A service result containing a paged result of deliveries or an error if an exception occurs.</returns>
    public async Task<IServiceResult<IPagedResult<Delivery>>> GetPagedDeliveryList(PagingInfo pagingInfo)
    {
        return await ServiceHelper.GetPagedResultAsync(
                logger,
                dbContext.DeliveryList, 
                pagingInfo.Page, 
                pagingInfo.Take);
    }
    
    /// <summary>
    /// Asynchronously gets a list of deliveries for a specific pharmacy.
    /// </summary>
    /// <param name="pharmacyId">The ID of the pharmacy to retrieve deliveries for.</param>
    /// <returns>A service result containing an asynchronous enumerable of deliveries or an error if an exception occurs.</returns>
    public Task<IServiceResult<IAsyncEnumerable<Delivery>>> GetDeliveryListByPharmacyId(int pharmacyId)
    {

        var deliveryListByPharmacy = dbContext.DeliveryList
            .Where(d => d.Pharmacy.Id == pharmacyId)
            .AsAsyncEnumerable();

        return Task.FromResult(ServiceHelper.BuildSuccessServiceResult(deliveryListByPharmacy));

        //return await deliveryListByPharmacy.AnyAsync()
        //    ? ServiceHelper.BuildSuccessServiceResult(deliveryListByPharmacy)
        //    : ServiceHelper.BuildNoContentResult<IAsyncEnumerable<Delivery>>(
        //            $"No deliveries found for the pharmacy with id {pharmacyId}");
        
    }

    /// <summary>
    /// Asynchronously gets a list of deliveries for a specific warehouse.
    /// </summary>
    /// <param name="warehouseId">The ID of the warehouse to retrieve deliveries for.</param>
    /// <returns>A service result containing an asynchronous enumerable of deliveries or an error if an exception occurs.</returns>
    public Task<IServiceResult<IAsyncEnumerable<Delivery>>> GetDeliveryListByWarehouseId(int warehouseId)
    {

        var deliveryListByWarehouse = dbContext.DeliveryList
            .Where(d => d.Warehouse.Id == warehouseId)
            .AsAsyncEnumerable();

        //var hasDeliveries = await deliveryListByWarehouse.AnyAsync();

        return Task.FromResult(ServiceHelper.BuildSuccessServiceResult(deliveryListByWarehouse));

        //return hasDeliveries
        //    ? ServiceHelper.BuildSuccessServiceResult(deliveryListByWarehouse)
        //    : ServiceHelper.BuildNoContentResult<IAsyncEnumerable<Delivery>>(
        //        $"No deliveries found for the warehouse with id {warehouseId}");        
    }

    /// <summary>
    /// Asynchronously inserts a new delivery record.
    /// </summary>
    /// <param name="delivery">The delivery object to insert.</param>
    /// <returns>A service result containing the inserted delivery or an error if an exception occurs.</returns>
    public async Task<IServiceResult<Delivery>> InsertDeliveryAsync(Delivery delivery)
    {
        var warehouseExists = await dbContext.WarehouseList.FindAsync(delivery.WarehouseId);
        var pharmacyExists = await dbContext.PharmacyList.FindAsync(delivery.PharmacyId);

        if (warehouseExists is null || pharmacyExists is null)
        {
            logger.LogWarning("Either pharmacy: {pharmacyId} or warehouse: {warehouseId} does not exist", 
                                delivery.PharmacyId, delivery.WarehouseId);

            return ServiceHelper.BuildErrorServiceResult<Delivery>(null, "inserting delievery");
        }

        dbContext.DeliveryList.Add(delivery);
        await dbContext.SaveChangesAsync();

        logger.LogDebug($"Successfully inserted delivery with ID: {delivery.Id}");
        return ServiceHelper.BuildSuccessServiceResult(delivery);
    }
}

