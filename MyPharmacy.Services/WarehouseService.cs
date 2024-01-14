using Microsoft.Extensions.Logging;
using MyPharmacy.Core.Helpers;
using MyPharmacy.Core.Utilities.Interfaces;
using MyPharmacy.Data;
using MyPharmacy.Data.Entities;
using MyPharmacy.Services.Interfaces;

namespace MyPharmacy.Services;

/// <summary>
/// Represents a service for managing warehouses.
/// </summary>
public class WarehouseService(
    ILogger<WarehouseService> logger, 
    IPharmacyDbContext dbContext) : IWarehouseService
{
    /// <summary>
    /// Retrieves a list of warehouses asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the service result with the list of warehouses.</returns>
    public async Task<IServiceResult<IAsyncEnumerable<Warehouse>>> GetWarehouseListAsync()
    {
        var warehouseList = dbContext.WarehouseList.AsAsyncEnumerable();

        //var hasWarehouses = await warehouseList.AnyAsync();

        return ServiceHelper.BuildSuccessServiceResult(warehouseList);
        //return hasWarehouses
        //    ? ServiceHelper.BuildSuccessServiceResult(warehouseList)
        //    : ServiceHelper.BuildNoContentResult<IAsyncEnumerable<Warehouse>>(
        //        "No warehouses found");
    }

    /// <summary>
    /// Inserts a new warehouse asynchronously.
    /// </summary>
    /// <param name="newWarehouse">The new warehouse to insert.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the service result with the inserted warehouse.</returns>
    public async Task<IServiceResult<Warehouse>> InsertWarehouseAsync(Warehouse newWarehouse)
    {
        logger.LogDebug("Initiating warehouse insert with {@newWarehouse}", newWarehouse);

        if (await dbContext.WarehouseList.FindAsync(newWarehouse.Id) is not null)
        {
            logger.LogWarning("Warehouse with id {id} already exists", newWarehouse.Id);
            return ServiceHelper.BuildErrorServiceResult<Warehouse>(new Exception(),
                @$"Warehouse with id {newWarehouse.Id} already exists. Use update endpoint to modify an existing record");
        }

        await dbContext.WarehouseList.AddAsync(newWarehouse);
        await dbContext.SaveChangesAsync();

        logger.LogDebug("Inserted new warehouse record: {@warehouse}", newWarehouse);
        return ServiceHelper.BuildSuccessServiceResult(newWarehouse);
    }

    /// <summary>
    /// Updates an existing warehouse asynchronously.
    /// </summary>
    /// <param name="updatedWarehouse">The updated warehouse.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the service result with the updated warehouse.</returns>
    public async Task<IServiceResult<Warehouse>> UpdateWarehouseAsync(Warehouse updatedWarehouse)
    {
        var searchResult = await GetWarehouseByIdAsync(updatedWarehouse.Id);

        if (searchResult.IsSuccess is false)
        {
            return searchResult;
        }

        var existingWarehouse = searchResult.Result;
        UpdateExistingWarehouse(existingWarehouse, updatedWarehouse);

        await dbContext.SaveChangesAsync();

        logger.LogDebug("Updated warehouse record: {@warehouse} with changes from request {@updateContent}",
            existingWarehouse, updatedWarehouse);
        return ServiceHelper.BuildSuccessServiceResult(existingWarehouse);
    }

    /// <summary>
    /// Retrieves a warehouse by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the warehouse to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the service result with the retrieved warehouse.</returns>
    public async Task<IServiceResult<Warehouse>> GetWarehouseByIdAsync(int id)
    {
        logger.LogDebug("Searching for warehouse with id {id}", id);
        var warehouse = await dbContext.WarehouseList.FindAsync(id);

        if (warehouse is null)
        {
            logger.LogWarning("No warehouse found with id {id}", id);
            return ServiceHelper.BuildNoContentResult<Warehouse>($"No warehouse found with id {id}");
        }

        logger.LogDebug("Found warehouse record {@warehouse} with id {id}", warehouse, id);
        return ServiceHelper.BuildSuccessServiceResult(warehouse);
    }

    private static void UpdateExistingWarehouse(Warehouse existingWarehouse, Warehouse updatedWarehouse)
    {
        existingWarehouse.Name    = updatedWarehouse.Name    ?? existingWarehouse.Name;
        existingWarehouse.Address = updatedWarehouse.Address ?? existingWarehouse.Address;
        existingWarehouse.City    = updatedWarehouse.City    ?? existingWarehouse.City;
        existingWarehouse.State   = updatedWarehouse.State   ?? existingWarehouse.State;
        existingWarehouse.Zip     = updatedWarehouse.Zip     ?? existingWarehouse.Zip;
    }
}

