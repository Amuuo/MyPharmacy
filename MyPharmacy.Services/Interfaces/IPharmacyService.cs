using MyPharmacy.Core.Utilities;
using MyPharmacy.Core.Utilities.Interfaces;
using MyPharmacy.Data.Entities;

namespace MyPharmacy.Services.Interfaces;

public interface IPharmacyService
{
    Task<IServiceResult<IPagedResult<Pharmacy>?>> GetPharmacyListPagedAsync(PagingInfo pagingInfo);
    Task<IServiceResult<Pharmacy>> UpdatePharmacyAsync(Pharmacy updatedPharmacy);
    Task<IServiceResult<Pharmacy>> GetPharmacyByIdAsync(int id);
    Task<IServiceResult<Pharmacy>> GetPharmacyByIdAsync2(int id);
    Task<IServiceResult<Pharmacy>> InsertPharmacyAsync(Pharmacy? newPharmacy);
    Task<IServiceResult<IAsyncEnumerable<Pharmacy>>> GetPharmaciesByPharmacistIdAsync(int pharmacistId);
}

