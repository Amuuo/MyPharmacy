using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MyPharmacy.Core.Helpers;
using MyPharmacy.Core.Utilities;
using MyPharmacy.Core.Utilities.Interfaces;
using MyPharmacy.Data;
using MyPharmacy.Data.Entities;
using MyPharmacy.Data.Repository.Interfaces;
using MyPharmacy.Services.Interfaces;

namespace MyPharmacy.Services;

public class PharmacyService(
    ILogger<IPharmacyService> logger, 
    IPharmacyDbContext pharmacyDbContext, 
    IPharmacyRepository pharmacyRepository, 
    IMemoryCache cache) : IPharmacyService
{
    /// <summary>
    /// Asynchronously retrieves pharmacy records based on the search criteria.
    /// </summary>
    /// <param name="pagedSearch">The search criteria object.</param>
    /// <param name="pagingInfo"></param>
    /// <returns>
    /// A ServiceResult containing IAsyncEnumerable of Pharmacy objects if any match the search criteria,
    /// or an error message if no matching pharmacies are found or if an exception occurs during retrieval.
    /// </returns>
    public async Task<IServiceResult<IPagedResult<Pharmacy>?>> GetPharmacyListPagedAsync(PagingInfo pagingInfo)
    {
        var cacheKey = $"PharmacyList_{pagingInfo.Page}_{pagingInfo.Take}";

        var stats = cache.GetCurrentStatistics();
        //logger.LogDebug("Cache stats: {@stats} for {@cache}", stats, cache);

        if (cache.TryGetValue(cacheKey, out IPagedResult<Pharmacy>? pharmacyListPaged))
        {
            //logger.LogDebug("Retrieved pharmacy list from cache {@pharmacyList}", pharmacyListPaged);
            return ServiceHelper.BuildSuccessServiceResult(pharmacyListPaged);
        }

        pharmacyListPaged = await pharmacyRepository.GetPharmacyListPagedAsync(pagingInfo);
        var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
        cache.Set(cacheKey, pharmacyListPaged, cacheEntryOptions);

        //logger.LogDebug("Pharmacy list was cached {@pharmacyList}", pharmacyListPaged);

        return ServiceHelper.BuildSuccessServiceResult(pharmacyListPaged);
    }
    
    /// <summary>
    /// Asynchronously updates a pharmacy record by its unique identifier with the provided details.
    /// </summary>
    /// <param name="updatedPharmacy">The updated pharmacy object containing the new details.</param>
    /// <returns>
    /// A ServiceResult containing the updated Pharmacy object if successful,
    /// or an error message if no pharmacy is found with the given id or if an exception occurs during the update.
    /// </returns>
    public async Task<IServiceResult<Pharmacy>> UpdatePharmacyAsync(Pharmacy updatedPharmacy)
    {
        var existingPharmacy = await pharmacyRepository.UpdatePharmacyAsync(updatedPharmacy);

        logger.LogDebug("Updated pharmacy record: {@pharmacy} with changes from request {@updateContent}", 
                         existingPharmacy, updatedPharmacy);

        return ServiceHelper.BuildSuccessServiceResult(existingPharmacy);
    }

    /// <summary>
    /// Asynchronously retrieves a pharmacy record by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the pharmacy record to retrieve.</param>
    /// <returns>
    /// A ServiceResult containing the Pharmacy object if found, 
    /// or an error message if no pharmacy record with the specified id exists.
    /// </returns>
    public async Task<IServiceResult<Pharmacy>> GetPharmacyByIdAsync(int id)
    {
        logger.LogDebug("Searching for pharmacy with id {id}", id);

        var pharmacy = await pharmacyRepository.GetByIdAsync(id);

        if (pharmacy is not null)
        {
            logger.LogDebug("Found pharmacy record {@pharmacy} with id {id}", pharmacy, id);
            return ServiceHelper.BuildSuccessServiceResult(pharmacy);
        }

        logger.LogWarning("No pharmacy found with id {id}", id);
        return ServiceHelper.BuildNoContentResult<Pharmacy>($"No pharmacy found with id {id}");
    }

    public async Task<IServiceResult<Pharmacy>> GetPharmacyByIdAsync2(int id)
    {
        logger.LogDebug("Searching for pharmacy with id {id}", id);

        //var pharmacy = await pharmacyDbContext.PharmacyList.FindAsync(id);
        var pharmacy = await pharmacyRepository.GetByIdAsync(id);

        if (pharmacy is null)
        {
            logger.LogWarning("No pharmacy found with id {id}", id);
            return ServiceHelper.BuildNoContentResult<Pharmacy>($"No pharmacy found with id {id}");
        }

        logger.LogDebug("Found pharmacy record {@pharmacy} with id {id}", pharmacy, id);

        return ServiceHelper.BuildSuccessServiceResult(pharmacy);
    }

    /// <summary>
    /// Asynchronously inserts a new pharmacy record.
    /// </summary>
    /// <param name="newPharmacy">The new pharmacy object to insert.</param>
    /// <returns>
    /// A ServiceResult containing the inserted Pharmacy object if successful,
    /// or an error message if an exception occurs during the insertion.
    /// </returns>
    public async Task<IServiceResult<Pharmacy>> InsertPharmacyAsync(Pharmacy newPharmacy)
    {
        logger.LogDebug("Initiating pharmacy insert with {@newPharmacy}", newPharmacy);

        var resultPharmacy = await pharmacyRepository.InsertPharmacyAsync(newPharmacy);

        return ServiceHelper.BuildSuccessServiceResult(resultPharmacy);
    }

    /// <summary>
    /// Asynchronously retrieves pharmacies associated with a specific pharmacist using the relationship table.
    /// </summary>
    /// <param name="pharmacistId">The unique identifier of the pharmacist.</param>
    /// <returns> 
    /// A ServiceResult containing IAsyncEnumerable of Pharmacy objects associated with the specified pharmacist,
    /// or an error message if no pharmacies are found or if an exception occurs during retrieval.
    /// </returns>
    public Task<IServiceResult<IAsyncEnumerable<Pharmacy>>> GetPharmaciesByPharmacistIdAsync(int pharmacistId)
    {
        logger.LogDebug("Searching for pharmacies associated with pharmacist with ID: {Id}", pharmacistId);

        var pharmacyList = pharmacyDbContext.PharmacyPharmacists
            .Where(pp => pp.PharmacistId == pharmacistId)
            .Select(pp => pp.Pharmacy)
            .AsAsyncEnumerable();

        logger.LogDebug("Found pharmacies associated with {pharmacistId}: {@pharmacyList}", pharmacistId, pharmacyList);
        return Task.FromResult(ServiceHelper.BuildSuccessServiceResult(pharmacyList));
    }


    #region Private Methods



    private static void UpdateExistingPharmacy(Pharmacy existingPharmacy, Pharmacy updatedPharmacy)
    {
        existingPharmacy.ModifiedDate = DateTime.Now;
        existingPharmacy.Name = updatedPharmacy.Name ?? existingPharmacy.Name;
        existingPharmacy.Address = updatedPharmacy.Address ?? existingPharmacy.Address;
        existingPharmacy.City = updatedPharmacy.City ?? existingPharmacy.City;
        existingPharmacy.State = updatedPharmacy.State ?? existingPharmacy.State;
        existingPharmacy.Zip = updatedPharmacy.Zip ?? existingPharmacy.Zip;
        existingPharmacy.PrescriptionsFilled = updatedPharmacy.PrescriptionsFilled 
                                               ?? existingPharmacy.PrescriptionsFilled;
    }


    #endregion
}
