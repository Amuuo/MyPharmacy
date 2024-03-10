using System.Data;
using System.Runtime.InteropServices;
using Dapper;
using Microsoft.AspNetCore.Http;
using MyPharmacy.Core.Utilities;
using MyPharmacy.Core.Utilities.Interfaces;
using MyPharmacy.Data.Entities;
using MyPharmacy.Data.Repository.Interfaces;

namespace MyPharmacy.Data.Repository;

/// <summary>
/// Represents a repository for managing pharmacists in the database.
/// </summary>
public class PharmacistRepository(
    IDbConnection dbConnection, IHttpContextAccessor contextAccessor) : IPharmacistRepository
{
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

    /// <summary>
    /// Adds a new pharmacist to the database asynchronously.
    /// </summary>
    /// <param name="pharmacist">The pharmacist object to be added.</param>
    /// <returns>The inserted pharmacist object.</returns>
    public async Task<Pharmacist?> AddPharmacistAsync(Pharmacist pharmacist)
    {
        var insertedPharmacist = await dbConnection.QueryFirstOrDefaultAsync<Pharmacist>(
            sql: "spAddPharmacist", 
            new
            {
                pharmacist.FirstName,
                pharmacist.LastName,
                pharmacist.Age,
                pharmacist.HireDate,
                pharmacist.PrimaryRx,
                pharmacist.ModifiedBy
            }, 
            commandType: CommandType.StoredProcedure);

        return insertedPharmacist;
    }

    /// <summary>
    /// Retrieves the count of pharmacists in the database.
    /// </summary>
    /// <returns>The count of pharmacists.</returns>
    public Task<int> GetPharmacistListCount()
    {
        const string sql = """
                           SELECT
                               COUNT(*)
                           FROM
                               Pharmacist
                           """;

        var pharmacistListCount = dbConnection.QuerySingleAsync<int>(sql);

        return pharmacistListCount;
    }


    /// <summary>
    /// Retrieves a paged list of pharmacists asynchronously.
    /// </summary>
    /// <param name="pagingInfo">The paging information.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the paged list of pharmacists.</returns>
    public async Task<IPagedResult<Pharmacist>> GetPagedPharmacistListAsync(PagingInfo pagingInfo)
    {
        var parameters = new DynamicParameters(new
        {
            pagingInfo.Page,
            pagingInfo.Take
        });

        parameters.Add("Count", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("Pages", dbType: DbType.Int32, direction: ParameterDirection.Output);

        var pharmacists = await dbConnection.QueryAsync<Pharmacist>(
            "spGetPagedPharmacistList", 
            parameters, 
            commandType: CommandType.StoredProcedure);

        return new PagedResult<Pharmacist>
        {
            Data       = pharmacists,
            Total      = parameters.Get<int>("Count"),
            Pages      = parameters.Get<int>("Pages"),
            PagingInfo = pagingInfo
        };
    }


    /// <summary>
    /// Retrieves a pharmacist by their ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the pharmacist.</param>
    /// <returns>The pharmacist object if found, otherwise null.</returns>
    public async Task<Pharmacist?> GetPharmacistByIdAsync(int id)
    {
        var pharmacist = await dbConnection.QueryFirstOrDefaultAsync<Pharmacist>(
            "spGetPharmacistById", 
            new
            {
                Id = id
            }, 
            commandType: CommandType.StoredProcedure);
        
        return pharmacist;
    }


    /// <summary>
    /// Retrieves a list of pharmacists based on the specified pharmacy ID asynchronously.
    /// </summary>
    /// <param name="pharmacyId">The ID of the pharmacy.</param>
    /// <returns>A collection of pharmacists.</returns>
    public async Task<IEnumerable<Pharmacist>?> GetPharmacistListByPharmacyIdAsync(int pharmacyId)
    {
        var pharmacist = await dbConnection.QueryAsync<Pharmacist>(
            "spGetPharmacistListByPharmacyId", 
            new
            {
                PharmacyId = pharmacyId
            }, 
            commandType: CommandType.StoredProcedure);

        return pharmacist;
    }



    /// <summary>
    /// Updates a pharmacist asynchronously.
    /// </summary>
    /// <param name="pharmacist">The pharmacist object to update.</param>
    /// <returns>The updated pharmacist object.</returns>
    public async Task<Pharmacist?> UpdatePharmacistAsync(Pharmacist pharmacist)
    {
        var updatedPharmacist = await dbConnection.QueryFirstOrDefaultAsync<Pharmacist>(
            "spUpdatePharmacist", 
            new
            {
                pharmacist.Id,
                pharmacist.FirstName,
                pharmacist.LastName,
                pharmacist.Age,
                pharmacist.HireDate,
                pharmacist.PrimaryRx
            }, 
            commandType: CommandType.StoredProcedure
        );

        return updatedPharmacist;
    }
}
