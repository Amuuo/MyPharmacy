using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Services.Interfaces;
using PharmacyApi.Utilities.Helpers;

namespace PharmacyApi.Controllers;

[ApiController]
[Route("delivery")]
public class DeliveryController : ControllerBase    
{
    #region Members and Constructor

    private readonly ILogger<DeliveryController> _logger;
    private readonly IDeliveryService _deliveryService;

    public DeliveryController(ILogger<DeliveryController> logger,
                              IDeliveryService deliveryService)
    {
        _logger = logger;
        _deliveryService = deliveryService;
    }

    #endregion

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> GetDeliveryList()
    {
        return (await _deliveryService.GetDeliveryList()).HandleResponse();
    }

    [HttpGet]
    [Route("pharmacy/{pharmacyId}")]
    public async Task<IActionResult> GetDeliveryListByPharmacyId(int pharmacyId)
    {
        return (await _deliveryService.GetDeliveryListByPharmacyId(pharmacyId)).HandleResponse();
    }

    [HttpGet]
    [Route("warehouse/{warehouseId}")]
    public async Task<IActionResult> GetDelieveryListByWarehouseId(int warehouseId)
    {
        return (await _deliveryService.GetDeliveryListByWarehouseId(warehouseId)).HandleResponse();
    }
}

