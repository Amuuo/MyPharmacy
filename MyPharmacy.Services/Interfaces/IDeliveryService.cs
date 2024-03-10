using MyPharmacy.Core.Utilities;
using MyPharmacy.Core.Utilities.Interfaces;
using MyPharmacy.Data.Entities;

namespace MyPharmacy.Services.Interfaces;

public interface IDeliveryService
{
    /// <summary>
    /// Asynchronously retrieves a paginated list of deliveries.
    /// </summary>
    /// <param name="pageNumber">The page number for pagination.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A service result containing a paged result of deliveries or an error if an exception occurs.</returns>
    Task<IServiceResult<IPagedResult<Delivery>>> GetPagedDeliveryList(PagingInfo pagingInfo);
    
    /// <summary>
    /// Asynchronously gets a list of deliveries for a specific pharmacy.
    /// </summary>
    /// <param name="pharmacyId">The ID of the pharmacy to retrieve deliveries for.</param>
    /// <returns>A service result containing an asynchronous enumerable of deliveries or an error if an exception occurs.</returns>
    Task<IServiceResult<IEnumerable<Delivery>>> GetDeliveryListByPharmacyId(int pharmacyId);
    
    /// <summary>
    /// Asynchronously gets a list of deliveries for a specific warehouse.
    /// </summary>
    /// <param name="warehouseId">The ID of the warehouse to retrieve deliveries for.</param>
    /// <returns>A service result containing an asynchronous enumerable of deliveries or an error if an exception occurs.</returns>
    Task<IServiceResult<IEnumerable<Delivery>>> GetDeliveryListByWarehouseId(int warehouseId);

    /// <summary>
    /// Asynchronously inserts a new delivery record.
    /// </summary>
    /// <param name="delivery">The delivery object to insert.</param>
    /// <returns>A service result containing the inserted delivery or an error if an exception occurs.</returns>
    Task<IServiceResult<Delivery>> InsertDeliveryAsync(Delivery delivery);
}
