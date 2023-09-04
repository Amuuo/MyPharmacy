using System.Net;
using Microsoft.EntityFrameworkCore;
using PharmacyApi.Data;
using PharmacyApi.Models;
using PharmacyApi.Services.Interfaces;
using PharmacyApi.Utilities;
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

                var hasPharmacists = await pharmacists.AnyAsync();

                if (hasPharmacists)
                {
                    _logger.LogDebug("Successfully retrieved all pharmacists.");
                    return new ServiceResult<IAsyncEnumerable<Pharmacist>>
                    {
                        IsSuccess  = true,
                        StatusCode = HttpStatusCode.OK,
                        Result     = pharmacists
                    };
                }

                _logger.LogWarning("No pharmacists found.");
                return new ServiceResult<IAsyncEnumerable<Pharmacist>>
                {
                    IsSuccess    = false,
                    StatusCode   = HttpStatusCode.NoContent,
                    ErrorMessage = "No pharmacists found"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all pharmacists.");
                return new ServiceResult<IAsyncEnumerable<Pharmacist>>
                {
                    IsSuccess    = false,
                    ErrorMessage = $"An error occurred while retrieving all pharmacists. Exception: {ex}",
                    StatusCode   = HttpStatusCode.InternalServerError
                };
            }
        }


        public Task<IServiceResult<Pharmacist>> GetPharmacistByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IServiceResult<IAsyncEnumerable<Pharmacist>>> GetPharmacistListByPharmacyIdAsync(int pharmacyId)
        {
            try
            {
                _logger.LogDebug("Attempting to retrieve pharmacists for pharmacy with ID {PharmacyId}", pharmacyId);

                var pharmacistList = _dbContext.PharmacyPharmacists
                    .Where(pp => pp.PharmacyId == pharmacyId)
                    .Select(pp => pp.Pharmacist)
                    .AsAsyncEnumerable();

                _logger.LogDebug("Retrieved pharmacists for pharmacy with ID {PharmacyId}.", pharmacyId);
                return new ServiceResult<IAsyncEnumerable<Pharmacist>>
                {
                    IsSuccess  = true,
                    StatusCode = HttpStatusCode.OK,
                    Result     = pharmacistList
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving pharmacists for pharmacy with ID {PharmacyId}.", pharmacyId);
                return new ServiceResult<IAsyncEnumerable<Pharmacist>>
                {
                    IsSuccess    = false,
                    ErrorMessage = $"An error occurred while retrieving pharmacists for pharmacy with ID {pharmacyId}. Exception: {ex}",
                    StatusCode   = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
