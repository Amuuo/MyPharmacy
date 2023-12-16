using Microsoft.EntityFrameworkCore;
using PharmacyApi.Data;
using PharmacyApi.Models;
using PharmacyApi.Services.Interfaces;
using PharmacyApi.Utilities.Helpers;
using PharmacyApi.Utilities.Interfaces;

namespace PharmacyApi.Services;

public class PharmacyService(
    ILogger<IPharmacyService> _logger, 
    IPharmacyDbContext _pharmacyDbContext) : IPharmacyService
{
    /// <summary>
    /// Asynchronously retrieves pharmacy records based on the search criteria.
    /// </summary>
    /// <param name="pagedSearch">The search criteria object.</param>
    /// <returns>
    /// A ServiceResult containing IAsyncEnumerable of Pharmacy objects if any match the search criteria,
    /// or an error message if no matching pharmacies are found or if an exception occurs during retrieval.
    /// </returns>
    public async Task<IServiceResult<IPagedResult<Pharmacy>>> 
        GetPharmacyListPagedAsync(int pageNumber, int pageSize)
    {
        return await ServiceHelper.GetPagedResultAsync(_logger, _pharmacyDbContext.PharmacyList, pageNumber, pageSize);
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
        var searchResult = await GetPharmacyByIdAsync(updatedPharmacy.Id);

        if (searchResult.IsSuccess is false)
        {
            return searchResult;
        }
        
        var existingPharmacy = searchResult.Result;

        UpdateExistingPharmacy(existingPharmacy, updatedPharmacy);
        
        try
        {
            await _pharmacyDbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while attempting to update pharmacy record");
            
            return ServiceHelper.BuildErrorServiceResult<Pharmacy>(ex, "updating a pharmacy record");
        }

        _logger.LogDebug("Updated pharmacy record: {@pharmacy} with changes from request {@updateContent}", 
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
        _logger.LogDebug("Searching for pharmacy with id {id}", id);

        var pharmacy = await _pharmacyDbContext.PharmacyList.FindAsync(id);

        if (pharmacy is null)
        {
            _logger.LogWarning("No pharmacy found with id {id}", id);
            return ServiceHelper.BuildNoContentResult<Pharmacy>($"No pharmacy found with id {id}");
        }

        _logger.LogDebug("Found pharmacy record {@pharmacy} with id {id}", pharmacy, id);
        
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
        _logger.LogDebug("Initiating pharmacy insert with {@newPharmacy}", newPharmacy);

        if (await _pharmacyDbContext.PharmacyList.FindAsync(newPharmacy.Id) is not null)
        {
            _logger.LogWarning("pharmacy with id {id} already exists", newPharmacy.Id);
            
            return ServiceHelper.BuildErrorServiceResult<Pharmacy>(new Exception(), 
                @$"pharmacy with id {newPharmacy.Id} already exists. 
                   Use update endpoint to modify an existing record");
        }

        try
        {
            newPharmacy.CreatedDate = DateTime.Now;
            newPharmacy.UpdatedDate = DateTime.Now;

            await _pharmacyDbContext.PharmacyList.AddAsync(newPharmacy);
            await _pharmacyDbContext.SaveChangesAsync();

            _logger.LogDebug("Inserted new pharmacy record: {@pharmacy}", newPharmacy);

            return ServiceHelper.BuildSuccessServiceResult(newPharmacy);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while attempting to insert pharmacy record");
            return ServiceHelper.BuildErrorServiceResult<Pharmacy>(ex, "inserting a pharmacy record");
        }
    }


    /// <summary>
    /// Asynchronously retrieves pharmacies associated with a specific pharmacist using the relationship table.
    /// </summary>
    /// <param name="pharmacistId">The unique identifier of the pharmacist.</param>
    /// <returns> 
    /// A ServiceResult containing IAsyncEnumerable of Pharmacy objects associated with the specified pharmacist,
    /// or an error message if no pharmacies are found or if an exception occurs during retrieval.
    /// </returns>
    public async Task<IServiceResult<IAsyncEnumerable<Pharmacy>>> GetPharmaciesByPharmacistIdAsync(int pharmacistId)
    {
        _logger.LogDebug("Searching for pharmacies associated with pharmacist with ID: {PharmacistId}", pharmacistId);
    
        try
        {
            var pharmacyList = _pharmacyDbContext.PharmacyPharmacists
                .Where(pp => pp.PharmacistId == pharmacistId)
                .Select(pp => pp.Pharmacy)
                .AsAsyncEnumerable();

            if (await pharmacyList.AnyAsync())
            {
                _logger.LogDebug("Found pharmacies associated with {pharmacistId}: {@pharmacyList}", pharmacistId, pharmacyList);
                return ServiceHelper.BuildSuccessServiceResult(pharmacyList);
            }

            _logger.LogWarning("No pharmacies found associated with pharmacist with ID: {PharmacistId}", pharmacistId);
            return ServiceHelper.BuildNoContentResult<IAsyncEnumerable<Pharmacy>>(
                $"No pharmacies found associated with pharmacist with id {pharmacistId}");

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while searching for pharmacies associated with pharmacist with ID: {PharmacistId}", pharmacistId);
            return ServiceHelper.BuildErrorServiceResult<IAsyncEnumerable<Pharmacy>>(ex, 
                $"searching for pharmacies associated with pharmacist with id {pharmacistId}");
        }
    }


    #region Private Methods



    private static void UpdateExistingPharmacy(Pharmacy existingPharmacy, Pharmacy updatedPharmacy)
    {
        existingPharmacy.UpdatedDate = DateTime.Now;
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
