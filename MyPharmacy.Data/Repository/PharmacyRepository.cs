using System.Data;
using MyPharmacy.Data.Entities;
using Dapper;
using MyPharmacy.Data.Repository.Interfaces;
using MyPharmacy.Core.Utilities;
using MyPharmacy.Core.Utilities.Interfaces;

namespace MyPharmacy.Data.Repository;

/// <summary>
/// Represents a repository for managing pharmacy data.
/// </summary>
public class PharmacyRepository(
    IDbConnection dbConnection) : IPharmacyRepository
{
    
    /// <summary>
    /// Retrieves a paged list of pharmacies asynchronously.
    /// </summary>
    /// <param name="pagingInfo">The paging information.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the paged result of pharmacies.</returns>
    public async Task<IPagedResult<Pharmacy>?> GetPharmacyListPagedAsync(PagingInfo pagingInfo)
    {
        var parameters = new DynamicParameters(new
        {
            pagingInfo.Page, 
            pagingInfo.Take,                        
        });

        parameters.Add("Count", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("Pages", dbType: DbType.Int32, direction: ParameterDirection.Output);

        var pharmacies = await dbConnection.QueryAsync<Pharmacy>(
            "spGetPagedPharmacyList",
            parameters,
            commandType: CommandType.StoredProcedure
        );

        return new PagedResult<Pharmacy>
        {
            Data       = pharmacies,
            PagingInfo = pagingInfo,
            Total      = parameters.Get<int>("Count"),
            Pages      = parameters.Get<int>("Pages")
        };
    }


    /// <summary>
    /// Retrieves a pharmacy by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the pharmacy to retrieve.</param>
    /// <returns>The pharmacy with the specified ID, or null if not found.</returns>
    public async Task<Pharmacy?> GetByIdAsync(int id)
    {
        var pharmacy = await dbConnection.QueryFirstOrDefaultAsync<Pharmacy>(
            "spGetPharmacyById",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        );

        return pharmacy;
    }


    /// <summary>
    /// Inserts a new pharmacy record asynchronously.
    /// </summary>
    /// <param name="pharmacy">The pharmacy object to be inserted.</param>
    /// <returns>The inserted pharmacy object.</returns>
    public async Task<Pharmacy?> InsertPharmacyAsync(Pharmacy pharmacy)
    {
        var insertedPharmacy = await dbConnection.QueryFirstOrDefaultAsync<Pharmacy>(
            "spInsertPharmacy",
            new
            {
                pharmacy.Name,
                pharmacy.Address,
                pharmacy.City,
                pharmacy.State,
                pharmacy.Zip,
                pharmacy.PrescriptionsFilled,
                pharmacy.ModifiedBy
            },
            commandType: CommandType.StoredProcedure
        );

        return insertedPharmacy;
    }


    /// <summary>
    /// Updates a pharmacy asynchronously.
    /// </summary>
    /// <param name="pharmacy">The pharmacy object to update.</param>
    /// <returns>The updated pharmacy object.</returns>
    public async Task<Pharmacy?> UpdatePharmacyAsync(Pharmacy pharmacy)
    {        
        var updatedPharmacy = await dbConnection.QueryFirstOrDefaultAsync<Pharmacy>(
            "spUpdatePharmacy",
            new
            {
                pharmacy.Id,
                pharmacy.Name,
                pharmacy.Address,
                pharmacy.City,
                pharmacy.State,
                pharmacy.Zip,
                pharmacy.PrescriptionsFilled,
                pharmacy.ModifiedBy 
            },
            commandType: CommandType.StoredProcedure
        );

        return updatedPharmacy;
    }

    public async Task<IEnumerable<Pharmacy>?> GetPharmaciesByPharmacistIdAsync(int pharmacistId)
    {
        var pharmacyList = await dbConnection.QueryAsync<Pharmacy>(
            "spGetPharmaciesByPharmacistId", 
            new
            {
                pharmacistId
            }, 
            commandType: CommandType.StoredProcedure
        );

        return pharmacyList;
    }
}