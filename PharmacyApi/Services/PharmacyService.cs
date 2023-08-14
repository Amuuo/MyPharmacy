using PharmacyApi.Data;
using PharmacyApi.Models;

namespace PharmacyApi.Services
{
    public class PharmacyService : IPharmacyService
    {
        #region Members and Constructor

        private readonly ILogger<PharmacyService> _logger;
        private readonly PharmacyDbContext _pharmacyDbContext;

        public PharmacyService(ILogger<PharmacyService> logger, 
                               PharmacyDbContext pharmacyDbContext)
        {
            _logger = logger;
            _pharmacyDbContext = pharmacyDbContext;
        }

        #endregion

        #region Public Methods
        
        public IEnumerable<Pharmacy> GetAll()
        {
            var pharmacies = _pharmacyDbContext.Pharmacies.ToList();

            _logger.LogDebug("Retrieved all pharmacies {@pharmacies}", pharmacies);

            return pharmacies;
        }

        public async Task<Pharmacy?> GetById(int id)
        {
            return await SearchById(id);
        }

        public async Task<Pharmacy?> UpdateById(int id, Pharmacy updatedPharmacy)
        {
            var pharmacyToUpdate = await SearchById(id);
            if (pharmacyToUpdate is null) return null;
            
            foreach(var property in typeof(Pharmacy).GetProperties())
            {
                var newValue = property.GetValue(updatedPharmacy);
                if (newValue is not null) property.SetValue(pharmacyToUpdate, newValue);
            }
            pharmacyToUpdate.UpdatedDate = DateTime.Now;
                
            await _pharmacyDbContext.SaveChangesAsync();

            _logger.LogDebug("Updated pharmacy record: {@pharmacy} with changes from request {@updateContent}", 
                             pharmacyToUpdate, updatedPharmacy);
            
            return pharmacyToUpdate;
        }

        #endregion

        #region Private Methods

        private async Task<Pharmacy?> SearchById(int id)
        {
            var pharmacy = await _pharmacyDbContext.Pharmacies.FindAsync(id);

            if (pharmacy is not null)
            {
                _logger.LogDebug("Found pharmacy record {@pharmacy} with id {id}", pharmacy, id);
                return pharmacy;
            }

            _logger.LogWarning("No pharmacy found with id {id}", id);
            return null;
        }

        #endregion
    }
}