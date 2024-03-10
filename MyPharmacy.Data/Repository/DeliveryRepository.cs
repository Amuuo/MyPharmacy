using System.Data;
using Dapper;
using MyPharmacy.Core.Utilities;
using MyPharmacy.Core.Utilities.Interfaces;
using MyPharmacy.Data.Entities;
using MyPharmacy.Data.Repository.Interfaces;

namespace MyPharmacy.Data.Repository;

public class DeliveryRepository(IDbConnection dbConnection) : IDeliveryRepository
{
    private readonly IDbConnection _dbConnection = dbConnection;

    public async Task<IPagedResult<Delivery>> GetPagedDeliveryListAsync(PagingInfo pagingInfo)
    {
        var parameters = new DynamicParameters(new
        {
            pagingInfo.Page, 
            pagingInfo.Take,                        
        });

        parameters.Add("Count", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("Pages", dbType: DbType.Int32, direction: ParameterDirection.Output);

        var deliveries = await _dbConnection.QueryAsync<Delivery>(
            sql: "spGetPagedDeliveryList", 
            parameters,
            commandType: CommandType.StoredProcedure);

        return new PagedResult<Delivery>
        {
            Data       = deliveries,
            PagingInfo = pagingInfo,
            Total      = parameters.Get<int>("Count"),
            Pages      = parameters.Get<int>("Pages")
        };
    }

    public async Task<Delivery?> AddDeliveryAsync(Delivery delivery)
    {
        var insertedDelivery = await _dbConnection.QueryFirstOrDefaultAsync<Delivery>(
            sql: "spAddDelivery", 
            new 
            {
                delivery.PharmacyId,
                delivery.DeliveryDate,
                delivery.ModifiedBy
            }, 
            commandType: CommandType.StoredProcedure);

        return insertedDelivery;
    }

    public async Task<Delivery?> UpdateDeliveryAsync(Delivery delivery)
    {
        var updatedDelivery = await _dbConnection.QueryFirstOrDefaultAsync<Delivery>(
            sql: "spUpdateDelivery", 
            new 
            {
                delivery.Id,
                delivery.PharmacyId,
                delivery.DeliveryDate,
                delivery.ModifiedBy
            }, 
            commandType: CommandType.StoredProcedure);

        return updatedDelivery;
    }

    public async Task<Delivery?> GetDeliveryByIdAsync(int id)
    {
        var delivery = await _dbConnection.QueryFirstOrDefaultAsync<Delivery>(
            sql: "spGetDeliveryById", 
            new
            {
                id
            }, 
            commandType: CommandType.StoredProcedure);

        return delivery;
    }

    public async Task<IEnumerable<Delivery>> GetDeliveryListByPharmacyIdAsync(int pharmacyId)
    {
        var deliveries = await _dbConnection.QueryAsync<Delivery>(
            sql: "spGetDeliveryListByPharmacyId", 
            new
            {
                PharmacyId = pharmacyId
            }, 
            commandType: CommandType.StoredProcedure);

        return deliveries;
    }

    //public async Task<int> GetDeliveryListCount()
    //{
    //    const string sql = @"
    //                        SELECT
    //                            COUNT(*)
    //                        FROM
    //                            Delivery
    //                        ";

    //    var deliveryListCount = await _dbConnection.QuerySingleAsync<int>(sql);

    //    return deliveryListCount;
    //}
}
