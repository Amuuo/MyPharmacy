using MyPharmacy.Core.Utilities;
using MyPharmacy.Core.Utilities.Interfaces;
using MyPharmacy.Data.Entities;

namespace MyPharmacy.Services.Interfaces;

public interface IPharmacistService
{
    Task<IServiceResult<IPagedResult<Pharmacist>>> GetPagedPharmacistListAsync(PagingInfo pagingInfo);
    Task<IServiceResult<Pharmacist>> GetPharmacistByIdAsync(int id);
    Task<IServiceResult<IAsyncEnumerable<Pharmacist>>> GetPharmacistListByPharmacyIdAsync(int pharmacyId);
    Task<IServiceResult<Pharmacist>> UpdatePharmacistAsync(Pharmacist pharmacist);
    Task<IServiceResult<Pharmacist>> AddPharmacistAsync(Pharmacist pharmacist);
}
