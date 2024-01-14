using MyPharmacy.Core.Utilities;
using MyPharmacy.Core.Utilities.Interfaces;
using MyPharmacy.Data.Entities;

namespace MyPharmacy.Services.Interfaces;

public interface IDeliveryService
{
    Task<IServiceResult<IPagedResult<Delivery>>> GetPagedDeliveryList(PagingInfo pagingInfo);
    Task<IServiceResult<IAsyncEnumerable<Delivery>>> GetDeliveryListByPharmacyId(int pharmacyId);
    Task<IServiceResult<IAsyncEnumerable<Delivery>>> GetDeliveryListByWarehouseId(int warehouseId);
    Task<IServiceResult<Delivery>> InsertDeliveryAsync(Delivery delivery);
}
