using PharmacyApi.Models;

namespace PharmacyApi.Services
{
    public interface IPharmacyService
    {
        IEnumerable<Pharmacy> GetPharmacies();
        Pharmacy GetPharmacyById(int id);
        void UpdatePharmacyById(int id, Pharmacy pharmacy);
    }
}
