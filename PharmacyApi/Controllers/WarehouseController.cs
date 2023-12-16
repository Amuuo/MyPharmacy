using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Models;
using PharmacyApi.Services.Interfaces;
using PharmacyApi.Utilities.Helpers;

namespace PharmacyApi.Controllers;

[ApiController]
[Route("warehouse")]
public class WarehouseController(
    ILogger<WarehouseController> logger, 
    IWarehouseService warehouseService) : ControllerBase
{
    private readonly ILogger<WarehouseController> _logger = logger;
    private readonly IWarehouseService _warehouseService = warehouseService;

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> GetWarehouseList() =>
        (await _warehouseService.GetWarehouseListAsync()).HandleResponse();

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetWarehouseById(int id) =>
        (await _warehouseService.GetWarehouseByIdAsync(id)).HandleResponse();

    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> UpdateWarehouse(Warehouse updatedWarehouse) =>
        (await _warehouseService.UpdateWarehouseAsync(updatedWarehouse)).HandleResponse();

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddWarehouse(Warehouse newWarehouse) =>
        (await _warehouseService.InsertWarehouseAsync(newWarehouse)).HandleResponse();

}

