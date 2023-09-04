using Microsoft.EntityFrameworkCore;
using PharmacyApi.Data;
using PharmacyApi.Models;
using PharmacyApi.Services.Interfaces;
using PharmacyApi.Utilities.Helpers;
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
            try
            {
                var deliveryList = _dbContext.DeliveryList.AsAsyncEnumerable();

                var hasDeliveries = await deliveryList.AnyAsync();

                return hasDeliveries
                    ? ServiceHelper.BuildSuccessServiceResult(deliveryList)
                    : ServiceHelper.BuildNoContentResult<IAsyncEnumerable<Delivery>>("No deliveries found");
            }
            catch (Exception ex)
            {
                return ServiceHelper
                    .BuildErrorServiceResult<IAsyncEnumerable<Delivery>>(ex, "searching for deliveries");
            }
        }

        public async Task<IServiceResult<IAsyncEnumerable<Delivery>>> GetDeliveryListByPharmacyId(int pharmacyId)
        {
            try
            {
                var deliveryListByPharmacy = _dbContext.DeliveryList
                    .Where(d => d.Pharmacy.Id == pharmacyId)
                    .AsAsyncEnumerable();

                var hasDeliveries = await deliveryListByPharmacy.AnyAsync();

                return hasDeliveries
                    ? ServiceHelper.BuildSuccessServiceResult(deliveryListByPharmacy)
                    : ServiceHelper.BuildNoContentResult<IAsyncEnumerable<Delivery>>(
                            $"No deliveries found for the pharmacy with id {pharmacyId}");
            }
            catch (Exception ex)
            {
                return ServiceHelper
                    .BuildErrorServiceResult<IAsyncEnumerable<Delivery>>(ex,
                        $"searching for deliveries for pharmacy with id {pharmacyId}");
            }
        }


        public async Task<IServiceResult<IAsyncEnumerable<Delivery>>> GetDeliveryListByWarehouseId(int warehouseId)
        {
            try
            {
                var deliveryListByWarehouse = _dbContext.DeliveryList
                    .Where(d => d.Warehouse.Id == warehouseId)
                    .AsAsyncEnumerable();

                var hasDeliveries = await deliveryListByWarehouse.AnyAsync();

                return hasDeliveries
                    ? ServiceHelper.BuildSuccessServiceResult(deliveryListByWarehouse)
                    : ServiceHelper.BuildNoContentResult<IAsyncEnumerable<Delivery>>(
                        $"No deliveries found for the warehouse with id {warehouseId}");
            }
            catch (Exception ex)
            {
                return ServiceHelper.BuildErrorServiceResult<IAsyncEnumerable<Delivery>>(ex,
                        $"searching for deliveries for warehouse with id {warehouseId}");
            }
        }

    }
}
