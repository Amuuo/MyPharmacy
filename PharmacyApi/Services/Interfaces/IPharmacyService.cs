using PharmacyApi.Models;
using PharmacyApi.Utilities;

namespace PharmacyApi.Services.Interfaces;

public interface IPharmacyService
{
    Task<ServiceResult<IAsyncEnumerable<Pharmacy>>> GetPharmacyListAsync();
    Task<ServiceResult<Pharmacy>> GetPharmacyByIdAsync(int id);
    Task<ServiceResult<Pharmacy>> UpdatePharmacyByIdAsync(Pharmacy updatedPharmacy);
}

