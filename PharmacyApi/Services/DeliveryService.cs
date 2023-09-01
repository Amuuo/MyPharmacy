using System.Net;
using Microsoft.EntityFrameworkCore;
using PharmacyApi.Data;
using PharmacyApi.Models;
using PharmacyApi.Services.Interfaces;
using PharmacyApi.Utilities;

namespace PharmacyApi.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly ILogger<DeliveryService> _logger;
        private readonly IPharmacyDbContext _dbContext;

        public DeliveryService(ILogger<DeliveryService> logger,
                               IPharmacyDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<ServiceResult<IAsyncEnumerable<Delivery>>> GetDeliveryList()
        {
            var deliveryList = _dbContext.DeliveryList
                .Include(d => d.Pharmacy)
                .Include(d => d.Warehouse)
                .AsAsyncEnumerable();

            var hasDeliveries = await deliveryList.AnyAsync();
            if (hasDeliveries)
            {
                return new ServiceResult<IAsyncEnumerable<Delivery>>
                {
                    IsSuccess = true, Result = deliveryList, StatusCode = HttpStatusCode.OK
                };
            }

            return new ServiceResult<IAsyncEnumerable<Delivery>>
            {
                IsSuccess    = false,
                ErrorMessage = "No deliveries found",
                StatusCode   = HttpStatusCode.NoContent
            };


        }
    }
}
