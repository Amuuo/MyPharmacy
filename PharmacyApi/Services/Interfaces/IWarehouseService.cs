using PharmacyApi.Models;
using PharmacyApi.Utilities.Interfaces;

namespace PharmacyApi.Services.Interfaces;

public interface IWarehouseService
{
    Task<IServiceResult<IAsyncEnumerable<Warehouse>>> GetWarehouseListAsync();
    Task<IServiceResult<Warehouse>> InsertWarehouseAsync(Warehouse newWarehouse);
    Task<IServiceResult<Warehouse>> UpdateWarehouseAsync(Warehouse updatedWarehouse);
    Task<IServiceResult<Warehouse>> GetWarehouseByIdAsync(int id);
}
