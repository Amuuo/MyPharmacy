using MyPharmacy.Core.Utilities.Interfaces;
using MyPharmacy.Data.Models;

namespace MyPharmacy.Services.Interfaces;

public interface IDeliveryService
{
    Task<IServiceResult<IPagedResult<Delivery>>> GetPagedDeliveryList(int pageNumber, int pageSize);
    Task<IServiceResult<IAsyncEnumerable<Delivery>>> GetDeliveryListByPharmacyId(int pharmacyId);
    Task<IServiceResult<IAsyncEnumerable<Delivery>>> GetDeliveryListByWarehouseId(int warehouseId);
    Task<IServiceResult<Delivery>> InsertDeliveryAsync(Delivery delivery);
}
