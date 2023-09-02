using System.Net;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PharmacyApi.Data;
using PharmacyApi.Models;
using PharmacyApi.Services.Interfaces;
using PharmacyApi.Utilities;
using PharmacyApi.Utilities.Interfaces;

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
    /// Asynchronously retrieves pharmacy records based on the search criteria.
    /// </summary>
    /// <param name="pagedSearch">The search criteria object.</param>
    /// <returns>
    /// A ServiceResult containing IAsyncEnumerable of Pharmacy objects if any match the search criteria,
    /// or an error message if no matching pharmacies are found or if an exception occurs during retrieval.
    /// </returns>
    public async Task<IServiceResult<IAsyncEnumerable<Pharmacy>>> SearchPharmacyAsync(PharmacyPagedSearch pagedSearch)
    {
        try
        {
            _logger.LogDebug("Attempting to query pharmacy table with {@pagedSearch}", pagedSearch);

            var pharmacyList = ExecuteSearchProcedure(pagedSearch);

            var hasResults = await pharmacyList.AnyAsync();
            if (hasResults is false)
            {
                return new ServiceResult<IAsyncEnumerable<Pharmacy>>
                {
                    IsSuccess    = false,
                    ErrorMessage = "No pharmacies found with the provided search criteria.",
                    StatusCode   = HttpStatusCode.NoContent
                };
            }
            _logger.LogDebug("Retrieved pharmacies.");
            return new ServiceResult<IAsyncEnumerable<Pharmacy>>
            {
                IsSuccess  = true,
                Result     = pharmacyList,
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while searching for pharmacies.");
            return new ServiceResult<IAsyncEnumerable<Pharmacy>>
            {
                IsSuccess    = false,
                ErrorMessage = $"An error occurred while searching for pharmacies, ex: {ex}",
                StatusCode   = HttpStatusCode.InternalServerError
            };
        }
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
        if (searchResult.IsSuccess is false) return searchResult;
        
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
                IsSuccess    = false,
                ErrorMessage = $"Something went wrong when trying to save changes, ex: {ex}",
                StatusCode   = HttpStatusCode.NoContent
            };
        }

        _logger.LogDebug("Updated pharmacy record: {@pharmacy} with changes from request {@updateContent}", 
                         existingPharmacy, updatedPharmacy);

        return new ServiceResult<Pharmacy>
        {
            IsSuccess  = true, 
            Result     = existingPharmacy, 
            StatusCode = HttpStatusCode.OK
        };
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
        var pharmacy = await _pharmacyDbContext.PharmacyList.FindAsync(id);

        if (pharmacy is not null)
        {
            _logger.LogDebug("Found pharmacy record {@pharmacy} with id {id}", pharmacy, id);
            return new ServiceResult<Pharmacy>
            {
                IsSuccess  = true, 
                Result     = pharmacy, 
                StatusCode = HttpStatusCode.OK
            };
        }

        _logger.LogWarning("No pharmacy found with id {id}", id);
        return new ServiceResult<Pharmacy>
        {
            IsSuccess    = false,
            ErrorMessage = $"No pharmacy found with id {id}",
            StatusCode   = HttpStatusCode.NoContent
        };
    }

    public async Task<IServiceResult<int>> GetPharmacyTotalCountAsync()
    {
        var totalCount = await _pharmacyDbContext.PharmacyList.CountAsync();

        return new ServiceResult<int>
        {
            StatusCode = HttpStatusCode.OK,
            IsSuccess  = true,
            Result     = totalCount
        };
    }

    #endregion


    #region Private Methods


    private IAsyncEnumerable<Pharmacy> ExecuteSearchProcedure(PharmacyPagedSearch pagedSearch)
    {
        var parameters = new SqlParameter[]
        {
            new("@SearchQuery",   pagedSearch.SearchCriteria.SearchQuery ?? (object)DBNull.Value),
            new("@PageNumber",    pagedSearch.PageNumber),
            new("@PageSize",      pagedSearch.PageSize),
            new("@SortColumn",    pagedSearch.SortColumn),
            new("@SortDirection", pagedSearch.SortDirection)
        };

        const string sql = @"EXEC 
                                sp_SearchPharmacyList 
                                    @SearchQuery, 
                                    @PageNumber, 
                                    @PageSize, 
                                    @SortColumn, 
                                    @SortDirection";

        return _pharmacyDbContext.PharmacyList
            .FromSqlRaw(sql, parameters)
            .AsAsyncEnumerable();
    }


    #endregion
}
