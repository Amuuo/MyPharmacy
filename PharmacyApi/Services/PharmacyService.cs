using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PharmacyApi.Data;
using PharmacyApi.Models;
using PharmacyApi.Services.Interfaces;
using PharmacyApi.Utilities;
using PharmacyApi.Utilities.Helpers;
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
    public async Task<IServiceResult<IPagedResult<Pharmacy>>> SearchPharmacyListAsync(PharmacyPagedSearch pagedSearch)
    {
        try
        {
            var pharmacyList = ExecutePharmacySearchProcedure(pagedSearch);

            if (await pharmacyList.AnyAsync() is false)
            {
                return ServiceHelper
                    .BuildNoContentResult<IPagedResult<Pharmacy>>("No pharmacies found with search criteria");
            }

            _logger.LogDebug("Retrieved pharmacies.");
            
            return await ServiceHelper
                .BuildPagedResultAsync(pharmacyList, 
                                       pagedSearch, 
                                       await _pharmacyDbContext.PharmacyList.CountAsync());
        }
        catch (Exception ex)
        {
            return ServiceHelper
                .BuildErrorServiceResult<IPagedResult<Pharmacy>>(ex, "searching for pharmacies");
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
        var pharmacy = await _pharmacyDbContext.PharmacyList.FindAsync(id);

        if (pharmacy is null)
        {
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
    public async Task<IServiceResult<Pharmacy>> InsertPharmacyAsync(Pharmacy? newPharmacy)
    {
        if (newPharmacy is null)
        {
            return ServiceHelper.BuildErrorServiceResult<Pharmacy>(null, "Pharmacy object is null");
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



    #endregion


    #region Private Methods


    private IAsyncEnumerable<Pharmacy> ExecutePharmacySearchProcedure(PharmacyPagedSearch pagedSearch)
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
