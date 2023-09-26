using Microsoft.EntityFrameworkCore;
using PharmacyApi.Data;
using PharmacyApi.Models;
using PharmacyApi.Services.Interfaces;
using PharmacyApi.Utilities;
using PharmacyApi.Utilities.Helpers;
using PharmacyApi.Utilities.Interfaces;

namespace PharmacyApi.Services;

public class DeliveryService : IDeliveryService
{
    private readonly ILogger<DeliveryService> _logger;
    private readonly IPharmacyDbContext _dbContext;

    public DeliveryService(ILogger<DeliveryService> logger,
                           IPharmacyDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<IServiceResult<IPagedResult<Delivery>>> GetPagedDeliveryList(int pageNumber, int pageSize)
    {
        try
        {
            return await ServiceHelper.GetPagedResultAsync(
                    _logger,
                    _dbContext.DeliveryList, 
                    pageNumber, 
                    pageSize);

        }
        catch (Exception ex)
        {
            return ServiceHelper
                .BuildErrorServiceResult<IPagedResult<Delivery>>(ex, "searching for deliveries");
        }
    }

    public async Task<IServiceResult<IAsyncEnumerable<Delivery>>> GetDeliveryListByPharmacyId(int pharmacyId)
    {
        try
        {
            var deliveryListByPharmacy = _dbContext.DeliveryList
                .Where(d => d.Pharmacy.Id == pharmacyId)
                .AsAsyncEnumerable();

            return await deliveryListByPharmacy.AnyAsync()
                ? ServiceHelper.BuildSuccessServiceResult(deliveryListByPharmacy)
                : ServiceHelper.BuildNoContentResult<IAsyncEnumerable<Delivery>>(
                        $"No deliveries found for the pharmacy with id {pharmacyId}");
        }
        catch (Exception ex)
        {
            return ServiceHelper
                .BuildErrorServiceResult<IAsyncEnumerable<Delivery>>(ex,
                    $"searching for deliveries for pharmacy with id {pharmacyId}");
        }
    }


    public async Task<IServiceResult<IAsyncEnumerable<Delivery>>> GetDeliveryListByWarehouseId(int warehouseId)
    {
        try
        {
            var deliveryListByWarehouse = _dbContext.DeliveryList
                .Where(d => d.Warehouse.Id == warehouseId)
                .AsAsyncEnumerable();

            var hasDeliveries = await deliveryListByWarehouse.AnyAsync();

            return hasDeliveries
                ? ServiceHelper.BuildSuccessServiceResult(deliveryListByWarehouse)
                : ServiceHelper.BuildNoContentResult<IAsyncEnumerable<Delivery>>(
                    $"No deliveries found for the warehouse with id {warehouseId}");
        }
        catch (Exception ex)
        {
            return ServiceHelper.BuildErrorServiceResult<IAsyncEnumerable<Delivery>>(ex,
                    $"searching for deliveries for warehouse with id {warehouseId}");
        }
    }


    public async Task<IServiceResult<Delivery>> InsertDeliveryAsync(Delivery delivery)
    {
        try
        {
            var warehouseExists = await _dbContext.WarehouseList.FindAsync(delivery.WarehouseId);
            var pharmacyExists = await _dbContext.PharmacyList.FindAsync(delivery.PharmacyId);

            if (warehouseExists is null || pharmacyExists is null)
            {
                _logger.LogWarning("Either pharmacy: {pharmacyId} or warehouse: {warehouseId} does not exist", 
                                   delivery.PharmacyId, delivery.WarehouseId);

                return ServiceHelper.BuildErrorServiceResult<Delivery>(null, "inserting delievery");
            }

            _dbContext.DeliveryList.Add(delivery);
            await _dbContext.SaveChangesAsync();

            _logger.LogDebug($"Successfully inserted delivery with ID: {delivery.Id}");
            return ServiceHelper.BuildSuccessServiceResult(delivery);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while inserting delivery");
            return ServiceHelper.BuildErrorServiceResult<Delivery>(ex, 
                "Error occurred while inserting delivery");
        }
    }
}

