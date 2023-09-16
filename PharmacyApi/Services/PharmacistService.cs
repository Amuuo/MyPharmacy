using Microsoft.EntityFrameworkCore;
using PharmacyApi.Data;
using PharmacyApi.Models;
using PharmacyApi.Services.Interfaces;
using PharmacyApi.Utilities.Helpers;
using PharmacyApi.Utilities.Interfaces;

namespace PharmacyApi.Services;

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


    public async Task<IServiceResult<Pharmacist>> GetPharmacistByIdAsync(int id)
    {
        var pharmacist = await _dbContext.PharmacistList.FindAsync(id);

        return ServiceHelper.BuildSuccessServiceResult(pharmacist);
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

    public async Task<IServiceResult<Pharmacist>> UpdatePharmacistAsync(Pharmacist pharmacistToUpdate)
    {
        try
        {
            _logger.LogDebug("Attempting to update pharmacist with ID {PharmacistId}.", pharmacistToUpdate.Id);

            var existingPharmacist = await _dbContext.PharmacistList.FindAsync(pharmacistToUpdate.Id);
            if (existingPharmacist is null)
            {
                _logger.LogWarning("No pharmacist found with ID {PharmacistId}.", pharmacistToUpdate.Id);
                return ServiceHelper.BuildNoContentResult<Pharmacist>($"No pharmacist found with ID {pharmacistToUpdate.Id}");
            }

            UpdateExistingPharmacist(existingPharmacist, pharmacistToUpdate);

            await _dbContext.SaveChangesAsync();

            _logger.LogDebug("Successfully updated pharmacist with ID {PharmacistId}.", pharmacistToUpdate.Id);
            return ServiceHelper.BuildSuccessServiceResult(existingPharmacist);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating pharmacist with ID {PharmacistId}.", pharmacistToUpdate.Id);
            return ServiceHelper.BuildErrorServiceResult<Pharmacist>(ex, $"updating pharmacist with ID {pharmacistToUpdate.Id}");
        }
    }

    public async Task<IServiceResult<Pharmacist>> AddPharmacistAsync(Pharmacist pharmacist)
    {
        _logger.LogDebug("Attempting to add new pharmacist {@pharmacist}", pharmacist);

        try
        {
            var newPharmacist = await _dbContext.PharmacistList.AddAsync(pharmacist);

            _logger.LogDebug("Successfully added pharmacist");

            await _dbContext.SaveChangesAsync();

            return ServiceHelper.BuildSuccessServiceResult(newPharmacist.Entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while attempting to add a new pharmacist");
            return ServiceHelper.BuildErrorServiceResult<Pharmacist>(
                ex, "Error occuered when attempting to add pharmacist");
        }
    }

    private static void UpdateExistingPharmacist(Pharmacist existingPharmacist,
                                                 Pharmacist pharmacistToUpdate)
    {
        existingPharmacist.FirstName = pharmacistToUpdate.FirstName ?? existingPharmacist.FirstName;
        existingPharmacist.LastName  = pharmacistToUpdate.LastName  ?? existingPharmacist.LastName;
        existingPharmacist.Age       = pharmacistToUpdate.Age       ?? existingPharmacist.Age;
        existingPharmacist.HireDate  = pharmacistToUpdate.HireDate  ?? existingPharmacist.HireDate;
        existingPharmacist.PrimaryRx = pharmacistToUpdate.PrimaryRx ?? existingPharmacist.PrimaryRx;
    }
}
