using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Services.Interfaces;
using PharmacyApi.Utilities.Helpers;

namespace PharmacyApi.Controllers
{
    [ApiController]
    [Route("delivery")]
    public class DeliveryController : ControllerBase    
    {
        private readonly ILogger<DeliveryController> _logger;
        private readonly IDeliveryService _deliveryService;

        public DeliveryController(ILogger<DeliveryController> logger,
                                  IDeliveryService deliveryService)
        {
            _logger = logger;
            _deliveryService = deliveryService;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> GetDeliveryList()
        {
            var result = await _deliveryService.GetDeliveryList();

            return ControllerHelper.HandleResponse(result);
        }

        [HttpGet]
        [Route("pharmacy/{pharmacyId}")]
        public async Task<IActionResult> GetDeliveryListByPharmacyId(int pharmacyId)
        {
            var result = await _deliveryService.GetDeliveryListByPharmacyId(pharmacyId);

            return ControllerHelper.HandleResponse(result);
        }

        [HttpGet]
        [Route("warehouse/{warehouseId}")]
        public async Task<IActionResult> GetDelieveryListByWarehouseId(int warehouseId)
        {
            var result = await _deliveryService.GetDeliveryListByWarehouseId(warehouseId);

            return ControllerHelper.HandleResponse(result);
        }
    }
}
