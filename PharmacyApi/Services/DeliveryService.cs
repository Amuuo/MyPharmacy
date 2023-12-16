using Microsoft.EntityFrameworkCore;
using PharmacyApi.Data;
using PharmacyApi.Models;
using PharmacyApi.Services.Interfaces;
using PharmacyApi.Utilities;
using PharmacyApi.Utilities.Helpers;
using PharmacyApi.Utilities.Interfaces;

namespace PharmacyApi.Services;

public class DeliveryService(
    ILogger<DeliveryService> logger, 
    IPharmacyDbContext dbContext) : IDeliveryService
{    
    private readonly ILogger<DeliveryService> _logger = logger;
    private readonly IPharmacyDbContext _dbContext = dbContext;
    
    /// <summary>
    /// Asynchronously retrieves a paginated list of deliveries.
    /// </summary>
    /// <param name="pageNumber">The page number for pagination.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A service result containing a paged result of deliveries or an error if an exception occurs.</returns>
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
    
    /// <summary>
    /// Asynchronously gets a list of deliveries for a specific pharmacy.
    /// </summary>
    /// <param name="pharmacyId">The ID of the pharmacy to retrieve deliveries for.</param>
    /// <returns>A service result containing an asynchronous enumerable of deliveries or an error if an exception occurs.</returns>
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

    /// <summary>
    /// Asynchronously gets a list of deliveries for a specific warehouse.
    /// </summary>
    /// <param name="warehouseId">The ID of the warehouse to retrieve deliveries for.</param>
    /// <returns>A service result containing an asynchronous enumerable of deliveries or an error if an exception occurs.</returns>
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

    /// <summary>
    /// Asynchronously inserts a new delivery record.
    /// </summary>
    /// <param name="delivery">The delivery object to insert.</param>
    /// <returns>A service result containing the inserted delivery or an error if an exception occurs.</returns>
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

