using Microsoft.AspNetCore.Mvc;
using MyPharmacy.Core.Helpers;
using MyPharmacy.Data.Models;
using MyPharmacy.Services.Interfaces;

namespace PharmacyApi.Controllers;

[ApiController]
[Route("delivery")]
public class DeliveryController(
    ILogger<DeliveryController> logger, 
    IDeliveryService deliveryService) : ControllerBase    
{
    private readonly ILogger<DeliveryController> _logger = logger;
    private readonly IDeliveryService _deliveryService = deliveryService;

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetDeliveryList(int pageNumber = 0, int pageSize = 10) =>
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

