using Microsoft.EntityFrameworkCore;
using PharmacyApi.Data;
using PharmacyApi.Models;
using PharmacyApi.Services.Interfaces;
using PharmacyApi.Utilities.Helpers;
using PharmacyApi.Utilities.Interfaces;

namespace PharmacyApi.Services
{
    public class PharmacistService : IPharmacistService
    {
        private readonly ILogger _logger;
        private readonly IPharmacyDbContext _dbContext;

        public PharmacistService(ILogger<IPharmacistService> logger,
                                 IPharmacyDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        public async Task<IServiceResult<IAsyncEnumerable<Pharmacist>>> GetPharmacistListAsync()
        {
            try
            {
                _logger.LogDebug("Attempting to retrieve all pharmacists.");

                var pharmacists = _dbContext.PharmacistList.AsAsyncEnumerable();

                if (await pharmacists.AnyAsync())
                {
                    _logger.LogDebug("Successfully retrieved all pharmacists.");

                    return ServiceHelper.BuildSuccessServiceResult(pharmacists);
                }

                _logger.LogWarning("No pharmacists found.");
                return ServiceHelper.BuildNoContentResult<IAsyncEnumerable<Pharmacist>>("No pharmacists found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all pharmacists.");

                return ServiceHelper.BuildErrorServiceResult<IAsyncEnumerable<Pharmacist>>(ex, 
                    "retrieving all pharmacists.");
            }
        }


        public Task<IServiceResult<Pharmacist>> GetPharmacistByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IServiceResult<IAsyncEnumerable<Pharmacist>>> 
            GetPharmacistListByPharmacyIdAsync(int pharmacyId)
        {
            try
            {
                _logger.LogDebug(@"Attempting to retrieve pharmacists 
                                   for pharmacy with ID {PharmacyId}", pharmacyId);

                var pharmacistList = _dbContext.PharmacyPharmacists
                    .Where(pp => pp.PharmacyId == pharmacyId)
                    .Select(pp => pp.Pharmacist)
                    .AsAsyncEnumerable();

                _logger.LogDebug("Retrieved pharmacists for pharmacy with ID {PharmacyId}.", pharmacyId);
                
                return ServiceHelper.BuildSuccessServiceResult(pharmacistList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, @"An error occurred while retrieving 
                                       pharmacists for pharmacy with ID {PharmacyId}.", pharmacyId);

                return ServiceHelper
                    .BuildErrorServiceResult<IAsyncEnumerable<Pharmacist>>(ex, 
                        $"retrieving pharmacists for pharmacy with ID {pharmacyId}");
            }
        }
    }
}
