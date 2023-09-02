using System.Net;
using PharmacyApi.Data;
using PharmacyApi.Models;
using PharmacyApi.Services.Interfaces;
using PharmacyApi.Utilities;
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
            var warehouseList = _dbContext.WarehouseList.AsAsyncEnumerable();

            var hasWarehouses = await warehouseList.AnyAsync();

            if (hasWarehouses)
            {
                return new ServiceResult<IAsyncEnumerable<Warehouse>>
                {
                    IsSuccess  = true,
                    StatusCode = HttpStatusCode.OK,
                    Result     = warehouseList
                };
            }

            return new ServiceResult<IAsyncEnumerable<Warehouse>>
            {
                IsSuccess    = false,
                StatusCode   = HttpStatusCode.NoContent,
                ErrorMessage = "No warehouses found"
            };
        }
    }
}
