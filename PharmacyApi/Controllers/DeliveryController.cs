using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Models;
using PharmacyApi.Services.Interfaces;
using PharmacyApi.Utilities;
using PharmacyApi.Utilities.Helpers;

namespace PharmacyApi.Controllers;

[ApiController]
[Route("delivery")]
public class DeliveryController(
    ILogger<DeliveryController> _logger, 
    IDeliveryService _deliveryService) : ControllerBase    
{
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetDeliveryList(int pageNumber = 1, int pageSize = 10) =>
        (await _deliveryService.GetPagedDeliveryList(pageNumber, pageSize)).HandleResponse();

    [HttpGet]
    [Route("by-pharmacy/{id}")]
    public async Task<IActionResult> GetDeliveryListByPharmacyId(int id) =>
        (await _deliveryService.GetDeliveryListByPharmacyId(id)).HandleResponse();
    
    [HttpGet]
    [Route("by-warehouse/{id}")]
    public async Task<IActionResult> GetDeliveryListByWarehouseId(int id) =>
        (await _deliveryService.GetDeliveryListByWarehouseId(id)).HandleResponse();

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddDelivery(Delivery newDelivery) =>
        (await _deliveryService.InsertDeliveryAsync(newDelivery)).HandleResponse();

}

