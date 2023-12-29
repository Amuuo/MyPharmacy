using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPharmacy.Core.Helpers;
using MyPharmacy.Core.Utilities.Interfaces;
using MyPharmacy.Data;
using MyPharmacy.Data.Models;
using MyPharmacy.Services.Interfaces;

namespace MyPharmacy.Services;


/// <summary>
/// Represents a service for managing pharmacists.
/// </summary>
public class PharmacistService(
    ILogger<IPharmacistService> logger, 
    IPharmacyDbContext dbContext) : IPharmacistService
{
    private readonly ILogger<IPharmacistService> _logger = logger;
    private readonly IPharmacyDbContext _dbContext = dbContext;
    
    /// <summary>
    /// Retrieves a paged list of pharmacists.
    /// </summary>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the paged list of pharmacists.</returns>
    public async Task<IServiceResult<IPagedResult<Pharmacist>>> 
        GetPagedPharmacistListAsync(int pageNumber, int pageSize)
    {
        return await ServiceHelper.GetPagedResultAsync(_logger, _dbContext.PharmacistList, pageNumber, pageSize);
    }

    /// <summary>
    /// Retrieves a pharmacist by ID.
    /// </summary>
    /// <param name="id">The ID of the pharmacist.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the pharmacist.</returns>
    public async Task<IServiceResult<Pharmacist>> 
        GetPharmacistByIdAsync(int id)
    {
        var pharmacist = await _dbContext.PharmacistList.FindAsync(id);

        return pharmacist is not null 
            ? ServiceHelper.BuildSuccessServiceResult(pharmacist)
            : ServiceHelper.BuildNoContentResult<Pharmacist>($"No pharmacist found with ID {id}");
    }

    /// <summary>
    /// Retrieves a list of pharmacists by pharmacy ID.
    /// </summary>
    /// <param name="pharmacyId">The ID of the pharmacy.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of pharmacists.</returns>
    public Task<IServiceResult<IAsyncEnumerable<Pharmacist>>> 
        GetPharmacistListByPharmacyIdAsync(int pharmacyId)
    {
        _logger.LogDebug(@"Attempting to retrieve pharmacists 
                            for pharmacy with ID {Id}", pharmacyId);

        var pharmacistList = _dbContext.PharmacyPharmacists
            .Where(pp => pp.PharmacyId == pharmacyId)
            .Select(pp => pp.Pharmacist)
            .AsAsyncEnumerable();

        _logger.LogDebug("Retrieved {@pharmacists} for pharmacy with ID {Id}.", pharmacistList, pharmacyId);
        
        return Task.FromResult(ServiceHelper.BuildSuccessServiceResult(pharmacistList));
    }

    /// <summary>
    /// Updates a pharmacist.
    /// </summary>
    /// <param name="pharmacistToUpdate">The pharmacist to update.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated pharmacist.</returns>
    public async Task<IServiceResult<Pharmacist>> 
        UpdatePharmacistAsync(Pharmacist pharmacistToUpdate)
    {
        _logger.LogDebug("Attempting to update pharmacist with ID {Id}.", pharmacistToUpdate.Id);

        var existingPharmacist = await _dbContext.PharmacistList.FindAsync(pharmacistToUpdate.Id);
        if (existingPharmacist is null)
        {
            _logger.LogWarning("No pharmacist found with ID {Id}.", pharmacistToUpdate.Id);
            
            return ServiceHelper
                .BuildNoContentResult<Pharmacist>(
                    $"No pharmacist found with ID {pharmacistToUpdate.Id}");
        }

        UpdateExistingPharmacist(existingPharmacist, pharmacistToUpdate);

        await _dbContext.SaveChangesAsync();

        _logger.LogDebug("Successfully updated pharmacist with ID {Id}.", pharmacistToUpdate.Id);
        return ServiceHelper.BuildSuccessServiceResult(existingPharmacist);
    }

    /// <summary>
    /// Adds a new pharmacist.
    /// </summary>
    /// <param name="pharmacist">The pharmacist to add.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the added pharmacist.</returns>
    public async Task<IServiceResult<Pharmacist>> 
        AddPharmacistAsync(Pharmacist pharmacist)
    {
        _logger.LogDebug("Attempting to add new pharmacist {@pharmacist}", pharmacist);

        var newPharmacist = await _dbContext.PharmacistList.AddAsync(pharmacist);

        _logger.LogDebug("Successfully added pharmacist");

        await _dbContext.SaveChangesAsync();

        return ServiceHelper.BuildSuccessServiceResult(newPharmacist.Entity);
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
