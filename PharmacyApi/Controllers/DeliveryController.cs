using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Services.Interfaces;
using PharmacyApi.Utilities;

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
        [Route("all")]
        public async Task<IActionResult> GetDeliveryList()
        {
            var result = await _deliveryService.GetDeliveryList();

            return ControllerHelper.HandleResponse(result);
        }
    }
}
