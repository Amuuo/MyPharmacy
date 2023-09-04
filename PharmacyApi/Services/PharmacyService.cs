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
    public async Task<IServiceResult<IPagedResult<Pharmacy>>> SearchPharmacyAsync(PharmacyPagedSearch pagedSearch)
    {
        try
        {
            var pharmacyList = ExecuteSearchProcedure(pagedSearch);

            if (await pharmacyList.AnyAsync() is false)
            {
                return BuildNoContentResult<IPagedResult<Pharmacy>>("No pharmacies found with search criteria");
            }

            return await BuildPagedResultAsync(pharmacyList, pagedSearch);
        }
        catch (Exception ex)
        {
            return BuildErrorServiceResult<IPagedResult<Pharmacy>>(ex, "searching for pharmacies");
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
            return BuildErrorServiceResult<Pharmacy>(ex, "updating a pharmacy record");
        }

        _logger.LogDebug("Updated pharmacy record: {@pharmacy} with changes from request {@updateContent}", 
                         existingPharmacy, updatedPharmacy);

        return BuildSuccessServiceResult(existingPharmacy);
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

        if (pharmacy is null)
        {
            return BuildNoContentResult<Pharmacy>($"No pharmacy found with id {id}");
        }

        _logger.LogDebug("Found pharmacy record {@pharmacy} with id {id}", pharmacy, id);
        
        return BuildSuccessServiceResult(pharmacy);
    }


    #endregion


    #region Private Methods


    private IAsyncEnumerable<Pharmacy> ExecuteSearchProcedure(PharmacyPagedSearch pagedSearch)
    {
        _logger.LogDebug("Attempting to query pharmacy table with {@pagedSearch}", pagedSearch);

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


    private IServiceResult<T> BuildNoContentResult<T>(string message)
    {
        _logger.LogWarning(message);

        return new ServiceResult<T>
        {
            IsSuccess    = false,
            ErrorMessage = message,
            StatusCode   = HttpStatusCode.OK
        };
    }


    private IServiceResult<T> BuildErrorServiceResult<T>(Exception ex, string operation)
    {
        _logger.LogError(ex, $"An error occurred while {operation}.");
    
        return new ServiceResult<T>
        {
            IsSuccess    = false,
            ErrorMessage = $"An error occurred while {operation}, ex: {ex}",
            StatusCode   = HttpStatusCode.InternalServerError
        };
    }

    private static IServiceResult<T> BuildSuccessServiceResult<T>(T result)
    {
        return new ServiceResult<T>
        {
            IsSuccess  = true,
            Result     = result,
            StatusCode = HttpStatusCode.OK
        };
    }


    private async Task<IServiceResult<IPagedResult<Pharmacy>>> 
        BuildPagedResultAsync(IAsyncEnumerable<Pharmacy> pharmacyList, 
                                       PharmacyPagedSearch pagedSearch)
    {
        _logger.LogDebug("Retrieved pharmacies.");

        var pagedResult = await PagedResultHelper
            .BuildPagedResultAsync(pharmacyList, 
                                   pagedSearch.PageNumber, 
                                   pagedSearch.PageSize, 
                                   await _pharmacyDbContext.PharmacyList.CountAsync());

        return BuildSuccessServiceResult(pagedResult);
    }


    private static void UpdateExistingPharmacy(Pharmacy existingPharmacy, Pharmacy updatedPharmacy)
    {
        existingPharmacy.UpdatedDate = DateTime.Now;
        existingPharmacy.Name = updatedPharmacy.Name ?? existingPharmacy.Name;
        existingPharmacy.Address = updatedPharmacy.Address ?? existingPharmacy.Address;
        existingPharmacy.City = updatedPharmacy.City ?? existingPharmacy.City;
        existingPharmacy.State = updatedPharmacy.State ?? existingPharmacy.State;
        existingPharmacy.PrescriptionsFilled = updatedPharmacy.PrescriptionsFilled 
                                               ?? existingPharmacy.PrescriptionsFilled;
    }


    #endregion
}
