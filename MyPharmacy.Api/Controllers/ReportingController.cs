using Microsoft.AspNetCore.Mvc;
using MyPharmacy.Core.Helpers;
using MyPharmacy.Services.Interfaces;

namespace PharmacyApi.Controllers;

[ApiController]
[Route("reporting")]
public class ReportingController(
    IReportingService reportingService, 
    ILogger<ReportingController> logger) : ControllerBase
{
    private readonly ILogger<ReportingController> _logger = logger;

    [HttpGet("warehouse-profits")]
    public async Task<IActionResult> GetWarehouseProfits() =>
        (await reportingService.GetWarehouseProfitAsync()).HandleResponse();
    
    [HttpGet("delivery-detail")]
    public async Task<IActionResult> GetDeliveryDetail() =>
        (await reportingService.GetDeliveryDetailAsync()).HandleResponse();
    
    [HttpGet("sales-summary")]
    public async Task<IActionResult> GetPharmacistSalesSummary() =>
        (await reportingService.GetPharmacistSalesSummaryAsync()).HandleResponse();
    
}

