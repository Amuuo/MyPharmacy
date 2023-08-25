using PharmacyApi.Data;
using PharmacyApi.Models;
using PharmacyApi.Utilities;

namespace PharmacyApi.Services;

public class PharmacyService : IPharmacyService
{
    #region Members and Constructor

    private readonly ILogger<PharmacyService> _logger;
    private readonly IPharmacyDbContext _pharmacyDbContext;

    public PharmacyService(ILogger<PharmacyService> logger, 
                           IPharmacyDbContext pharmacyDbContext)
    {
        _logger = logger;
        _pharmacyDbContext = pharmacyDbContext;
    }

    #endregion

    #region Public Methods
    
    /// <summary>
    /// Asynchronously retrieves the complete list of pharmacy records.
    /// </summary>
    /// <returns>
    /// A ServiceResult containing an IAsyncEnumerable of Pharmacy objects if any are found,
    /// or an error message if no pharmacies are found or if an exception occurs during retrieval.
    /// </returns>
    public async Task<ServiceResult<IAsyncEnumerable<Pharmacy>>> GetPharmacyListAsync()
    {
        try
        {
            var pharmacyList = _pharmacyDbContext.PharmacyList.AsAsyncEnumerable();
            
            var hasPharmacies = await pharmacyList.AnyAsync();
            if (hasPharmacies is false)
            {
                _logger.LogWarning("No pharmacies found.");
                return new ServiceResult<IAsyncEnumerable<Pharmacy>>
                {
                    Success = false, ErrorMessage = "No pharmacies found."
                };
            }

            _logger.LogDebug("Retrieved all pharmacies.");
            return new ServiceResult<IAsyncEnumerable<Pharmacy>> { Success = true, Result = pharmacyList };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving pharmacies.");
            return new ServiceResult<IAsyncEnumerable<Pharmacy>>
            {
                Success      = false,
                ErrorMessage = "An error occurred while retrieving pharmacies."
            };
        }
    }



    /// <summary>
    /// Asynchronously retrieves a pharmacy record by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the pharmacy record to retrieve.</param>
    /// <returns>
    /// A ServiceResult containing the Pharmacy object if found, 
    /// or an error message if no pharmacy record with the specified id exists.
    /// </returns>
    public async Task<ServiceResult<Pharmacy>> GetByIdAsync(int id)
    {
        var pharmacy = await _pharmacyDbContext.PharmacyList.FindAsync(id);

        if (pharmacy is not null)
        {
            _logger.LogDebug("Found pharmacy record {@pharmacy} with id {id}", pharmacy, id);
            return new ServiceResult<Pharmacy> { Success = true, Result = pharmacy };
        }

        _logger.LogWarning("No pharmacy found with id {id}", id);
        return new ServiceResult<Pharmacy>
        {
            Success      = false,
            ErrorMessage = $"No pharmacy found with id {id}"
        };
    }



    /// <summary>
    /// Asynchronously updates a pharmacy record by its unique identifier with the provided details.
    /// </summary>
    /// <param name="updatedPharmacy">The updated pharmacy object containing the new details.</param>
    /// <returns>
    /// A ServiceResult containing the updated Pharmacy object if successful,
    /// or an error message if no pharmacy is found with the given id or if an exception occurs during the update.
    /// </returns>
    public async Task<ServiceResult<Pharmacy>> UpdateByIdAsync(Pharmacy updatedPharmacy)
    {
        var searchResult = await GetByIdAsync(updatedPharmacy.Id);
        if (searchResult.Success is false) return searchResult;
        
        var existingPharmacy = searchResult.Result;

        existingPharmacy.UpdatedDate = DateTime.Now;
        existingPharmacy.Name        = updatedPharmacy.Name    ?? existingPharmacy.Name;
        existingPharmacy.Address     = updatedPharmacy.Address ?? existingPharmacy.Address;
        existingPharmacy.City        = updatedPharmacy.City    ?? existingPharmacy.City;
        existingPharmacy.State       = updatedPharmacy.State   ?? existingPharmacy.State;
        existingPharmacy.PrescriptionsFilled = updatedPharmacy.PrescriptionsFilled ??
                                               existingPharmacy.PrescriptionsFilled;
        try
        {
            await _pharmacyDbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error attempting to save {@record}", existingPharmacy);
            return new ServiceResult<Pharmacy>
            {
                Success = false,
                ErrorMessage = $"Something went wrong when trying to save changes, ex: {ex}"
            };
        }

        _logger.LogDebug("Updated pharmacy record: {@pharmacy} with changes from request {@updateContent}", 
                         existingPharmacy, updatedPharmacy);

        return new ServiceResult<Pharmacy> { Success = true, Result = existingPharmacy };
    }

    #endregion

}
