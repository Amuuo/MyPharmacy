using PharmacyApi.Models;
using PharmacyApi.Utilities;

namespace PharmacyApi.Services.Interfaces
{
    public interface IPharmacistService
    {
        Task<ServiceResult<IAsyncEnumerable<Pharmacist>>> GetPharmacistList();

    }
}
