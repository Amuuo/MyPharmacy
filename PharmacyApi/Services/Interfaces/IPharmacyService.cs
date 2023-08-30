using PharmacyApi.Models;
using PharmacyApi.Utilities;

namespace PharmacyApi.Services.Interfaces;

public interface IPharmacyService
{
    Task<ServiceResult<IAsyncEnumerable<Pharmacy>>> SearchPharmacyAsync(PharmacySearch searchCriteria);
    Task<ServiceResult<Pharmacy>> UpdatePharmacyAsync(Pharmacy updatedPharmacy);
}

