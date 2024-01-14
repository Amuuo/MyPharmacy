using Microsoft.AspNetCore.Mvc;
using MyPharmacy.Core.Helpers;
using MyPharmacy.Core.Utilities;
using MyPharmacy.Data.Entities;
using MyPharmacy.Services.Interfaces;

namespace PharmacyApi.Controllers;

[ApiController]
[Route("delivery")]
public class DeliveryController(
    ILogger<DeliveryController> logger, 
    IDeliveryService deliveryService) : ControllerBase    
{
    private readonly ILogger<DeliveryController> _logger = logger;

    [HttpGet]
    public async Task<IActionResult> GetDeliveryList([FromQuery] PagingInfo pagingInfo) =>
        (await deliveryService.GetPagedDeliveryList(pagingInfo)).HandleResponse();

    [HttpGet("by-pharmacy/{id}")]
    public async Task<IActionResult> GetDeliveryListByPharmacyId(int id) =>
        (await deliveryService.GetDeliveryListByPharmacyId(id)).HandleResponse();
    
    [HttpGet("by-warehouse/{id}")]
    public async Task<IActionResult> GetDeliveryListByWarehouseId(int id) =>
        (await deliveryService.GetDeliveryListByWarehouseId(id)).HandleResponse();

    [HttpPost("add")]
    public async Task<IActionResult> AddDelivery(Delivery newDelivery) =>
        (await deliveryService.InsertDeliveryAsync(newDelivery)).HandleResponse();

}

