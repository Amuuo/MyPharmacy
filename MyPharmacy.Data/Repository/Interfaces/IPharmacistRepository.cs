using MyPharmacy.Core.Utilities;
using MyPharmacy.Core.Utilities.Interfaces;
using MyPharmacy.Data.Entities;

namespace MyPharmacy.Data.Repository.Interfaces;

public interface IPharmacistRepository
{
    Task<IPagedResult<Pharmacist>> GetPagedPharmacistListAsync(PagingInfo pagingInfo);
    Task<Pharmacist?> GetPharmacistByIdAsync(int id);
    Task<IEnumerable<Pharmacist>?> GetPharmacistListByPharmacyIdAsync(int pharmacyId);
    Task<Pharmacist?> UpdatePharmacistAsync(Pharmacist pharmacist);
    Task<Pharmacist?> AddPharmacistAsync(Pharmacist pharmacist);
    Task<int> GetPharmacistListCount();
}
