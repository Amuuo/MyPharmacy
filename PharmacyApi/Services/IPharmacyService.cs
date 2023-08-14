using PharmacyApi.Models;

namespace PharmacyApi.Services
{
    public interface IPharmacyService
    {
        IEnumerable<Pharmacy> GetPharmacies();
        Task<Pharmacy?> GetPharmacyById(int id);
        Task<Pharmacy?> UpdatePharmacyById(int id, Pharmacy updatedPharmacy);
    }
}
