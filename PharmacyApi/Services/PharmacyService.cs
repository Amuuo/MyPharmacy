using PharmacyApi.Data;
using PharmacyApi.Models;

namespace PharmacyApi.Services
{
    public class PharmacyService : IPharmacyService
    {
        private readonly ILogger<PharmacyService> _logger;
        private readonly PharmacyDbContext _pharmacyDbContext;

        public PharmacyService(ILogger<PharmacyService> logger, 
                               PharmacyDbContext pharmacyDbContext)
        {
            _logger = logger;
            _pharmacyDbContext = pharmacyDbContext;
        }

        public IEnumerable<Pharmacy> GetPharmacies()
        {
            return _pharmacyDbContext.Pharmacies.ToList(); 
        }

        public async Task<Pharmacy?> GetPharmacyById(int id)
        {
            return await _pharmacyDbContext.Pharmacies.FindAsync(id);
        }

        public async Task<Pharmacy?> UpdatePharmacyById(int id, Pharmacy updatedPharmacy)
        {
            var pharmacyToUpdate = await _pharmacyDbContext.Pharmacies.FindAsync(id);

            if (pharmacyToUpdate is null)
                return pharmacyToUpdate;

            typeof(Pharmacy).GetProperties().ToList().ForEach(property =>
            {
                var newValue = property.GetValue(updatedPharmacy);

                if (newValue is not null)
                    property.SetValue(pharmacyToUpdate, newValue);

            });
                
            await _pharmacyDbContext.SaveChangesAsync();
            return pharmacyToUpdate;
        }
    }
}