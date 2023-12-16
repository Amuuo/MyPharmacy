using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Services.Interfaces;
using PharmacyApi.Utilities.Helpers;

namespace PharmacyApi.Controllers;

[ApiController]
[Route("reporting")]
public class ReportingController(
    IReportService _reportService, 
    ILogger<ReportingController> _logger) : ControllerBase
{
    [HttpGet]
    [Route("warehouse-profits")]
    public async Task<IActionResult> GetWarehouseProfits() =>
        (await _reportService.GetWarehouseProfitAsync()).HandleResponse();
    

    [HttpGet]
    [Route("delivery-detail")]
    public async Task<IActionResult> GetDeliveryDetail() =>
        (await _reportService.GetDeliveryDetailAsync()).HandleResponse();
    

    [HttpGet]
    [Route("sales-summary")]
    public async Task<IActionResult> GetPharmacistSalesSummary() =>
        (await _reportService.GetPharmacistSalesSummaryAsync()).HandleResponse();
    
}

