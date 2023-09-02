using PharmacyApi.Models;
using PharmacyApi.Utilities.Interfaces;

namespace PharmacyApi.Services.Interfaces
{
    public interface IDeliveryService
    {
        Task<IServiceResult<IAsyncEnumerable<Delivery>>> GetDeliveryList();
    }
}
