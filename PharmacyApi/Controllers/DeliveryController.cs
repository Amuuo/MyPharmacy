using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Models;
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
    public async Task<IActionResult> GetDeliveryList() =>
        (await _deliveryService.GetDeliveryList()).HandleResponse();
    
    [HttpGet]
    [Route("by-pharmacy/{pharmacyId}")]
    public async Task<IActionResult> GetDeliveryListByPharmacyId(int pharmacyId) =>
        (await _deliveryService.GetDeliveryListByPharmacyId(pharmacyId)).HandleResponse();
    
    [HttpGet]
    [Route("by-warehouse/{warehouseId}")]
    public async Task<IActionResult> GetDeliveryListByWarehouseId(int warehouseId) =>
        (await _deliveryService.GetDeliveryListByWarehouseId(warehouseId)).HandleResponse();

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddDelivery(Delivery newDelivery) =>
        (await _deliveryService.InsertDeliveryAsync(newDelivery)).HandleResponse();

}

