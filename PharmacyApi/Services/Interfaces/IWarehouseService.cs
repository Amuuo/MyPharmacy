using PharmacyApi.Models;
using PharmacyApi.Utilities;

namespace PharmacyApi.Services.Interfaces
{
    public interface IWarehouseService
    {
        Task<ServiceResult<IAsyncEnumerable<Warehouse>>> GetWarehouseListAsync();
    }
}
