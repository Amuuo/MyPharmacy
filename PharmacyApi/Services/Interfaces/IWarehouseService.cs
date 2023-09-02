using PharmacyApi.Models;
using PharmacyApi.Utilities.Interfaces;

namespace PharmacyApi.Services.Interfaces
{
    public interface IWarehouseService
    {
        Task<IServiceResult<IAsyncEnumerable<Warehouse>>> GetWarehouseListAsync();
    }
}
