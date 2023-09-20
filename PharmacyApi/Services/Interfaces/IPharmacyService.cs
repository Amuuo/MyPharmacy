using PharmacyApi.Models;
using PharmacyApi.Utilities;
using PharmacyApi.Utilities.Interfaces;

namespace PharmacyApi.Services.Interfaces;

public interface IPharmacyService
{
    Task<IServiceResult<IPagedResult<Pharmacy>>> GetPharmacyListPagedAsync(int pageNumber, int pageSize);
    Task<IServiceResult<Pharmacy>> UpdatePharmacyAsync(Pharmacy updatedPharmacy);
    Task<IServiceResult<Pharmacy>> GetPharmacyByIdAsync(int id);
    Task<IServiceResult<Pharmacy>> InsertPharmacyAsync(Pharmacy? newPharmacy);
    Task<IServiceResult<IAsyncEnumerable<Pharmacy>>> GetPharmaciesByPharmacistIdAsync(int pharmacistId);
}

