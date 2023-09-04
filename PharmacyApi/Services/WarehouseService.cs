using PharmacyApi.Data;
using PharmacyApi.Models;
using PharmacyApi.Services.Interfaces;
using PharmacyApi.Utilities.Helpers;
using PharmacyApi.Utilities.Interfaces;

namespace PharmacyApi.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly ILogger<WarehouseService> _logger;
        private readonly IPharmacyDbContext _dbContext;

        public WarehouseService(ILogger<WarehouseService> logger,
                                IPharmacyDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<IServiceResult<IAsyncEnumerable<Warehouse>>> GetWarehouseListAsync()
        {
            try
            {
                var warehouseList = _dbContext.WarehouseList.AsAsyncEnumerable();

                var hasWarehouses = await warehouseList.AnyAsync();

                return hasWarehouses
                    ? ServiceHelper.BuildSuccessServiceResult(warehouseList)
                    : ServiceHelper.BuildNoContentResult<IAsyncEnumerable<Warehouse>>(
                        "No warehouses found");
            }
            catch (Exception ex)
            {
                return ServiceHelper.BuildErrorServiceResult<IAsyncEnumerable<Warehouse>>(
                        ex, "searching for warehouses");
            }
        }
    }
}
