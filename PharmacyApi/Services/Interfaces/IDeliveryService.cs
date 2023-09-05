using PharmacyApi.Models;
using PharmacyApi.Utilities.Interfaces;

namespace PharmacyApi.Services.Interfaces;

public interface IDeliveryService
{
    Task<IServiceResult<IAsyncEnumerable<Delivery>>> GetDeliveryList();
    Task<IServiceResult<IAsyncEnumerable<Delivery>>> GetDeliveryListByPharmacyId(int pharmacyId);
    Task<IServiceResult<IAsyncEnumerable<Delivery>>> GetDeliveryListByWarehouseId(int warehouseId);
    Task<IServiceResult<Delivery>> InsertDeliveryAsync(Delivery delivery);
}
