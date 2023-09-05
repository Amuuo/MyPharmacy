using PharmacyApi.Models;
using PharmacyApi.Utilities;
using PharmacyApi.Utilities.Interfaces;

namespace PharmacyApi.Services.Interfaces;

public interface IPharmacyService
{
    Task<IServiceResult<IPagedResult<Pharmacy>>> SearchPharmacyListPagedAsync(PharmacyPagedSearch searchCriteriaCriteria);
    Task<IServiceResult<Pharmacy>> UpdatePharmacyAsync(Pharmacy updatedPharmacy);
    Task<IServiceResult<Pharmacy>> GetPharmacyByIdAsync(int id);
    Task<IServiceResult<Pharmacy>> InsertPharmacyAsync(Pharmacy? newPharmacy);
    Task<IServiceResult<IAsyncEnumerable<Pharmacy>>> GetPharmaciesByPharmacistIdAsync(int pharmacistId);
}

