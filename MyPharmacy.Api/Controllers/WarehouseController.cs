using Microsoft.AspNetCore.Mvc;
using MyPharmacy.Core.Helpers;
using MyPharmacy.Data.Entities;
using MyPharmacy.Services.Interfaces;

namespace PharmacyApi.Controllers;

[ApiController]
[Route("warehouse")]
public class WarehouseController(
    ILogger<WarehouseController> logger, 
    IWarehouseService warehouseService) : ControllerBase
{
    private readonly ILogger<WarehouseController> _logger = logger;

    [HttpPost]
    public async Task<IActionResult> GetWarehouseList()
        => (await warehouseService.GetWarehouseListAsync()).HandleResponse();
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetWarehouseById(int id) =>
        (await warehouseService.GetWarehouseByIdAsync(id)).HandleResponse();

    [HttpPut("update")]
    public async Task<IActionResult> UpdateWarehouse(Warehouse updatedWarehouse) =>
        (await warehouseService.UpdateWarehouseAsync(updatedWarehouse)).HandleResponse();

    [HttpPost("add")]
    public async Task<IActionResult> AddWarehouse(Warehouse newWarehouse) =>
        (await warehouseService.InsertWarehouseAsync(newWarehouse)).HandleResponse();

}

