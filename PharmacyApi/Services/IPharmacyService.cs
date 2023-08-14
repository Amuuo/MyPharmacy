using PharmacyApi.Models;

namespace PharmacyApi.Services
{
    public interface IPharmacyService
    {
        IEnumerable<Pharmacy> GetAll();
        Task<Pharmacy?> GetById(int id);
        Task<Pharmacy?> UpdateById(int id, Pharmacy updatedPharmacy);
    }
}
