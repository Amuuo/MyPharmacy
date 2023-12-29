using MyPharmacy.Core.Utilities.Interfaces;
using MyPharmacy.Data.Models;

namespace MyPharmacy.Services.Interfaces;

public interface IWarehouseService
{
    Task<IServiceResult<IAsyncEnumerable<Warehouse>>> GetWarehouseListAsync();
    Task<IServiceResult<Warehouse>> InsertWarehouseAsync(Warehouse newWarehouse);
    Task<IServiceResult<Warehouse>> UpdateWarehouseAsync(Warehouse updatedWarehouse);
    Task<IServiceResult<Warehouse>> GetWarehouseByIdAsync(int id);
}
