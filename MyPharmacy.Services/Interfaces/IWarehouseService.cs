using MyPharmacy.Core.Utilities.Interfaces;
using MyPharmacy.Data.Entities;

namespace MyPharmacy.Services.Interfaces;

public interface IWarehouseService
{
    /// <summary>
    /// Retrieves a list of warehouses asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the service result with the list of warehouses.</returns>
    Task<IServiceResult<IAsyncEnumerable<Warehouse>>> GetWarehouseListAsync();
    
    /// <summary>
    /// Inserts a new warehouse asynchronously.
    /// </summary>
    /// <param name="newWarehouse">The new warehouse to insert.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the service result with the inserted warehouse.</returns>
    Task<IServiceResult<Warehouse>> InsertWarehouseAsync(Warehouse newWarehouse);
    
    /// <summary>
    /// Updates an existing warehouse asynchronously.
    /// </summary>
    /// <param name="updatedWarehouse">The updated warehouse.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the service result with the updated warehouse.</returns>
    Task<IServiceResult<Warehouse>> UpdateWarehouseAsync(Warehouse updatedWarehouse);
    
    /// <summary>
    /// Retrieves a warehouse by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the warehouse to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the service result with the retrieved warehouse.</returns>
    Task<IServiceResult<Warehouse>> GetWarehouseByIdAsync(int id);
}
