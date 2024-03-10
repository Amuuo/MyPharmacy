using MyPharmacy.Core.Utilities;
using MyPharmacy.Core.Utilities.Interfaces;
using MyPharmacy.Data.Entities;

namespace MyPharmacy.Data.Repository.Interfaces;
public interface IDeliveryRepository
{
    Task<IPagedResult<Delivery>> GetPagedDeliveryListAsync(PagingInfo pagingInfo);
    Task<IEnumerable<Delivery>> GetDeliveryListByPharmacyIdAsync(int pharmacyId);
}
