using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MyPharmacy.Core.Helpers;
using MyPharmacy.Core.Utilities;
using MyPharmacy.Core.Utilities.Interfaces;
using MyPharmacy.Data;
using MyPharmacy.Data.Entities;
using MyPharmacy.Data.Repository.Interfaces;
using MyPharmacy.Services.Interfaces;

namespace MyPharmacy.Services;

public class DeliveryService(
    ILogger<DeliveryService> logger, 
    IPharmacyDbContext dbContext, 
    IMemoryCache cache, 
    IDeliveryRepository deliveryRepository) : IDeliveryService
{

    /// <inheritdoc/>
    public async Task<IServiceResult<IPagedResult<Delivery>>> GetPagedDeliveryList(PagingInfo pagingInfo)
    {
        var cacheKey = $"PagedDeliveryList_{pagingInfo.Page}_{pagingInfo.Take}";

        var cachedResult = await cache.GetOrCreateAsync(cacheKey, x =>
        {
            x.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
            return deliveryRepository.GetPagedDeliveryListAsync(pagingInfo);
        });

        return ServiceHelper.BuildSuccessServiceResult(cachedResult);
    }
    

    /// <inheritdoc/>
    public async Task<IServiceResult<IEnumerable<Delivery>>> GetDeliveryListByPharmacyId(int pharmacyId)
    {
        var cacheKey = $"DeliveryListByPharmacy_{pharmacyId}";

        var cachedResult = await cache.GetOrCreateAsync(cacheKey, x =>
        {
            x.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
            return deliveryRepository.GetDeliveryListByPharmacyIdAsync(pharmacyId);
        });
        
        return ServiceHelper.BuildSuccessServiceResult(cachedResult);
    }


    /// <inheritdoc/>
    public Task<IServiceResult<IEnumerable<Delivery>>> GetDeliveryListByWarehouseId(int warehouseId)
    {

        var deliveryListByWarehouse = dbContext.DeliveryList
            .Where(d => d.Warehouse.Id == warehouseId)
            .AsEnumerable();

        return Task.FromResult(ServiceHelper.BuildSuccessServiceResult(deliveryListByWarehouse));
    }


    /// <inheritdoc/>
    public async Task<IServiceResult<Delivery>> InsertDeliveryAsync(Delivery delivery)
    {
        var warehouseExists = await dbContext.WarehouseList.AnyAsync(w => w.Id == delivery.WarehouseId);
        var pharmacyExists = await dbContext.PharmacyList.AnyAsync(p => p.Id == delivery.PharmacyId);

        if (!warehouseExists || !pharmacyExists)
        {
            return ServiceHelper.BuildErrorServiceResult<Delivery>(null, "inserting delievery");
        }

        dbContext.DeliveryList.Add(delivery);
        await dbContext.SaveChangesAsync();

        return ServiceHelper.BuildSuccessServiceResult(delivery);
    }
}

