using PharmacyApi.Models;
using PharmacyApi.Utilities;

namespace PharmacyApi.Services;

public interface IPharmacyService
{
    Task<ServiceResult<IAsyncEnumerable<Pharmacy>>> GetPharmacyListAsync();
    Task<ServiceResult<Pharmacy>> GetByIdAsync(int id);
    Task<ServiceResult<Pharmacy>> UpdateByIdAsync(Pharmacy updatedPharmacy);
}

