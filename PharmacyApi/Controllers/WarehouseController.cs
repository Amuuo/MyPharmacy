using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Services.Interfaces;
using PharmacyApi.Utilities.Helpers;

namespace PharmacyApi.Controllers
{
    [ApiController]
    [Route("warehouse")]
    public class WarehouseController
    {
        private readonly ILogger<WarehouseController> _logger;
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(ILogger<WarehouseController> logger, 
                                   IWarehouseService warehouseService)
        {
            _logger = logger;
            _warehouseService = warehouseService;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> GetWarehouseList()
        {
            var result = await _warehouseService.GetWarehouseListAsync();

            return ControllerHelper.HandleResponse(result);
        }
    }
}
