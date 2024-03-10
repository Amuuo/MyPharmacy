using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MyPharmacy.Core.Helpers;
using MyPharmacy.Core.Utilities;
using MyPharmacy.Core.Utilities.Interfaces;
using MyPharmacy.Data;
using MyPharmacy.Data.Entities;
using MyPharmacy.Data.Repository;
using MyPharmacy.Data.Repository.Interfaces;
using MyPharmacy.Services.Interfaces;

namespace MyPharmacy.Services;


/// <summary>
/// Represents a service for managing pharmacists.
/// </summary>
public class PharmacistService(
    ILogger<IPharmacistService> logger, 
    IPharmacyDbContext dbContext,
    IPharmacistRepository pharmacistRepository, 
    IMemoryCache cache) : IPharmacistService
{
    /// <summary>
    /// Retrieves a paged list of pharmacists.
    /// </summary>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the paged list of pharmacists.</returns>
    public async Task<IServiceResult<IPagedResult<Pharmacist>>> GetPagedPharmacistListAsync(PagingInfo pagingInfo)
    {
        var cacheKey = $"PagedPharmacistList_{pagingInfo.Page}_{pagingInfo.Take}";
        
        if (cache.TryGetValue(cacheKey, out IPagedResult<Pharmacist> cachedList))
        {
            return ServiceHelper.BuildSuccessServiceResult(cachedList);
        }

        cachedList = await pharmacistRepository.GetPagedPharmacistListAsync(pagingInfo);
        cache.Set(cacheKey, cachedList, TimeSpan.FromMinutes(5)); // Cache for 5 minutes

        return ServiceHelper.BuildSuccessServiceResult(cachedList);
    }

    /// <summary>
    /// Retrieves a pharmacist by ID.
    /// </summary>
    /// <param name="id">The ID of the pharmacist.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the pharmacist.</returns>
    public async Task<IServiceResult<Pharmacist?>> GetPharmacistByIdAsync(
        int id
    )
    {
        var cacheKey = $"Pharmacist_{id}";
        
        if (cache.TryGetValue(cacheKey, out Pharmacist? pharmacist))
        {
            return (pharmacist is not null
                ? ServiceHelper.BuildSuccessServiceResult(pharmacist)
                : ServiceHelper.BuildNoContentResult<Pharmacist>(
                    $"No pharmacist found with ID {id}"))!;
        }

        pharmacist = await pharmacistRepository.GetPharmacistByIdAsync(id);
        cache.Set(cacheKey, pharmacist, TimeSpan.FromMinutes(5)); // Cache for 5 minutes

        return (pharmacist is not null 
            ? ServiceHelper.BuildSuccessServiceResult(pharmacist)
            : ServiceHelper.BuildNoContentResult<Pharmacist>($"No pharmacist found with ID {id}"))!;
    }


    /// <summary>
    /// Retrieves a list of pharmacists by pharmacy ID.
    /// </summary>
    /// <param name="pharmacyId">The ID of the pharmacy.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of pharmacists.</returns>
    public async Task<IServiceResult<IEnumerable<Pharmacist>?>> GetPharmacistListByPharmacyIdAsync(int pharmacyId)
    {
        logger.LogDebug(@"Attempting to retrieve pharmacists 
                            for pharmacy with ID {Id}", pharmacyId);

        var cacheKey = $"PharmacistListByPharmacy_{pharmacyId}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<Pharmacist>? pharmacistList))
        {
            pharmacistList = await pharmacistRepository.GetPharmacistListByPharmacyIdAsync(pharmacyId);
            cache.Set(cacheKey, pharmacistList, TimeSpan.FromMinutes(5)); // Cache for 5 minutes
        }

        logger.LogDebug("Retrieved {@pharmacists} for pharmacy with ID {Id}.", pharmacistList, pharmacyId);
        
        return ServiceHelper.BuildSuccessServiceResult(pharmacistList);
    }


    /// <summary>
    /// Updates a pharmacist.
    /// </summary>
    /// <param name="pharmacistToUpdate">The pharmacist to update.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated pharmacist.</returns>
    public async Task<IServiceResult<Pharmacist>> UpdatePharmacistAsync(Pharmacist pharmacistToUpdate)
    {
        logger.LogDebug("Attempting to update pharmacist with ID {Id}.", pharmacistToUpdate.Id);

        var existingPharmacist = await pharmacistRepository.GetPharmacistByIdAsync(pharmacistToUpdate.Id);

        //var existingPharmacist = await dbContext.PharmacistList.FindAsync(pharmacistToUpdate.Id);
        if (existingPharmacist is null)
        {
            logger.LogWarning("No pharmacist found with ID {Id}.", pharmacistToUpdate.Id);
            
            return ServiceHelper
                .BuildNoContentResult<Pharmacist>(
                    $"No pharmacist found with ID {pharmacistToUpdate.Id}");
        }

        var pharmacist = await pharmacistRepository.UpdatePharmacistAsync(pharmacistToUpdate);

        // UpdateExistingPharmacist(existingPharmacist, pharmacistToUpdate);

        // await dbContext.SaveChangesAsync();

        logger.LogDebug("Successfully updated pharmacist with ID {Id}.", pharmacistToUpdate.Id);
        return ServiceHelper.BuildSuccessServiceResult(pharmacist);
    }


    /// <summary>
    /// Adds a new pharmacist.
    /// </summary>
    /// <param name="pharmacist">The pharmacist to add.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the added pharmacist.</returns>
    public async Task<IServiceResult<Pharmacist>> AddPharmacistAsync(Pharmacist pharmacist)
    {
        logger.LogDebug("Attempting to add new pharmacist {@pharmacist}", pharmacist);

        var newPharmacist = await pharmacistRepository.AddPharmacistAsync(pharmacist);
        //var newPharmacist = await dbContext.PharmacistList.AddAsync(pharmacist);

        logger.LogDebug("Successfully added pharmacist");

        //await dbContext.SaveChangesAsync();

        return ServiceHelper.BuildSuccessServiceResult(newPharmacist);
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
