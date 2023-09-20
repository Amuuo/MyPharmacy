using PharmacyApi.Models;
using PharmacyApi.Utilities;
using PharmacyApi.Utilities.Interfaces;

namespace PharmacyApi.Services.Interfaces;

public interface IDeliveryService
{
    Task<IServiceResult<IPagedResult<Delivery>>> GetPagedDeliveryList(int pageNumber, int pageSize);
    Task<IServiceResult<IAsyncEnumerable<Delivery>>> GetDeliveryListByPharmacyId(int pharmacyId);
    Task<IServiceResult<IAsyncEnumerable<Delivery>>> GetDeliveryListByWarehouseId(int warehouseId);
    Task<IServiceResult<Delivery>> InsertDeliveryAsync(Delivery delivery);
}
