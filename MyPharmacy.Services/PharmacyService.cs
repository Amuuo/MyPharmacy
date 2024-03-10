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

    /// <inheritdoc/>
    public async Task<IServiceResult<IPagedResult<Pharmacy>?>> GetPharmacyListPagedAsync(PagingInfo pagingInfo)
    {
        var cacheKey = $"PharmacyList_{pagingInfo.Page}_{pagingInfo.Take}";

        var cachedPharmacyListPaged = await cache.GetOrCreateAsync(cacheKey, x => 
        {
            x.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
            return pharmacyRepository.GetPharmacyListPagedAsync(pagingInfo);
        });

        return ServiceHelper.BuildSuccessServiceResult(cachedPharmacyListPaged);
    }
    

    /// <inheritdoc/>
    public async Task<IServiceResult<Pharmacy?>> UpdatePharmacyAsync(Pharmacy updatedPharmacy)
    {
        var existingPharmacy = await pharmacyRepository.UpdatePharmacyAsync(updatedPharmacy);

        return ServiceHelper.BuildSuccessServiceResult(existingPharmacy);
    }


    /// <inheritdoc/>
    public async Task<IServiceResult<Pharmacy?>> GetPharmacyByIdAsync(int id)
    {
        var cachedPharmacy = await cache.GetOrCreateAsync($"Pharmacy_{id}", x =>
        {
            x.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
            return pharmacyRepository.GetByIdAsync(id);
        });

        return cachedPharmacy is not null 
            ? ServiceHelper.BuildSuccessServiceResult(cachedPharmacy) 
            : ServiceHelper.BuildNoContentResult<Pharmacy>($"No pharmacy found with id {id}");
    }


    /// <inheritdoc/>
    public async Task<IServiceResult<Pharmacy?>> InsertPharmacyAsync(Pharmacy newPharmacy)
    {
        var resultPharmacy = await pharmacyRepository.InsertPharmacyAsync(newPharmacy);

        return ServiceHelper.BuildSuccessServiceResult(resultPharmacy);
    }


    /// <inheritdoc/>
    public async Task<IServiceResult<IEnumerable<Pharmacy>?>> GetPharmaciesByPharmacistIdAsync(int pharmacistId)
    {
        var cacheKey = $"PharmacistPharmacyList_{pharmacistId}";

        var pharmacyList = await cache.GetOrCreateAsync(cacheKey, x =>
        {
            x.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
            return pharmacyRepository.GetPharmaciesByPharmacistIdAsync(pharmacistId);
        });

        return ServiceHelper.BuildSuccessServiceResult(pharmacyList);
    }


    #region Private Methods



    private static void UpdateExistingPharmacy(Pharmacy existingPharmacy, Pharmacy updatedPharmacy)
    {
        existingPharmacy.ModifiedDate        = DateTime.Now;
        existingPharmacy.Name                = updatedPharmacy.Name    ?? existingPharmacy.Name;
        existingPharmacy.Address             = updatedPharmacy.Address ?? existingPharmacy.Address;
        existingPharmacy.City                = updatedPharmacy.City    ?? existingPharmacy.City;
        existingPharmacy.State               = updatedPharmacy.State   ?? existingPharmacy.State;
        existingPharmacy.Zip                 = updatedPharmacy.Zip     ?? existingPharmacy.Zip;
        existingPharmacy.PrescriptionsFilled = updatedPharmacy.PrescriptionsFilled 
                                               ?? existingPharmacy.PrescriptionsFilled;
    }


    #endregion
}
