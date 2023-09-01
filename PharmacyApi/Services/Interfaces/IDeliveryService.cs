using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Models;
using PharmacyApi.Utilities;

namespace PharmacyApi.Services.Interfaces
{
    public interface IDeliveryService
    {
        Task<ServiceResult<IAsyncEnumerable<Delivery>>> GetDeliveryList();
    }
}
