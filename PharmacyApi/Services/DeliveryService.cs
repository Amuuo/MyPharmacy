using System.Net;
using Microsoft.EntityFrameworkCore;
using PharmacyApi.Data;
using PharmacyApi.Models;
using PharmacyApi.Services.Interfaces;
using PharmacyApi.Utilities;
using PharmacyApi.Utilities.Interfaces;

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

        public async Task<IServiceResult<IAsyncEnumerable<Delivery>>> GetDeliveryList()
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

        public async Task<IServiceResult<IAsyncEnumerable<Delivery>>> GetDeliveryListByPharmacyId(int pharmacyId)
        {
            var deliveryListByPharmacy = _dbContext.DeliveryList
                //.Include(d => d.Pharmacy)
                //.Include(d => d.Warehouse)
                .Where(d => d.Pharmacy.Id == pharmacyId)
                .AsAsyncEnumerable();

            var hasDeliveries = await deliveryListByPharmacy.AnyAsync();
            if (hasDeliveries)
            {
                return new ServiceResult<IAsyncEnumerable<Delivery>>
                {
                    IsSuccess = true, Result = deliveryListByPharmacy, StatusCode = HttpStatusCode.OK
                };
            }

            return new ServiceResult<IAsyncEnumerable<Delivery>>
            {
                IsSuccess    = false,
                ErrorMessage = "No deliveries found for the given pharmacy",
                StatusCode   = HttpStatusCode.NoContent
            };
        }


        public async Task<IServiceResult<IAsyncEnumerable<Delivery>>> GetDeliveryListByWarehouseId(int warehouseId)
        {
            var deliveryListByWarehouse = _dbContext.DeliveryList
                //.Include(d => d.Pharmacy)
                //.Include(d => d.Warehouse)
                .Where(d => d.Warehouse.Id == warehouseId)
                .AsAsyncEnumerable();

            var hasDeliveries = await deliveryListByWarehouse.AnyAsync();
            if (hasDeliveries)
            {
                return new ServiceResult<IAsyncEnumerable<Delivery>>
                {
                    IsSuccess = true, Result = deliveryListByWarehouse, StatusCode = HttpStatusCode.OK
                };
            }

            return new ServiceResult<IAsyncEnumerable<Delivery>>
            {
                IsSuccess    = false,
                ErrorMessage = "No deliveries found for the given warehouse",
                StatusCode   = HttpStatusCode.NoContent
            };
        }

    }
}
