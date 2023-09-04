using PharmacyApi.Models;
using PharmacyApi.Utilities;
using PharmacyApi.Utilities.Interfaces;

namespace PharmacyApi.Services.Interfaces;

public interface IPharmacyService
{
    Task<IServiceResult<IPagedResult<Pharmacy>>> SearchPharmacyAsync(PharmacyPagedSearch searchCriteriaCriteria);
    Task<IServiceResult<Pharmacy>> UpdatePharmacyAsync(Pharmacy updatedPharmacy);
    Task<IServiceResult<Pharmacy>> GetPharmacyByIdAsync(int id);

}

