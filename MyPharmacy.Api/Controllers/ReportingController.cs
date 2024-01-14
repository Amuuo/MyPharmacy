using Microsoft.AspNetCore.Mvc;
using MyPharmacy.Core.Helpers;
using MyPharmacy.Services.Interfaces;

namespace PharmacyApi.Controllers;

[ApiController]
[Route("reporting")]
public class ReportingController(
    IReportService reportService, 
    ILogger<ReportingController> logger) : ControllerBase
{
    private readonly ILogger<ReportingController> _logger = logger;

    [HttpGet("warehouse-profits")]
    public async Task<IActionResult> GetWarehouseProfits() =>
        (await reportService.GetWarehouseProfitAsync()).HandleResponse();
    
    [HttpGet("delivery-detail")]
    public async Task<IActionResult> GetDeliveryDetail() =>
        (await reportService.GetDeliveryDetailAsync()).HandleResponse();
    
    [HttpGet("sales-summary")]
    public async Task<IActionResult> GetPharmacistSalesSummary() =>
        (await reportService.GetPharmacistSalesSummaryAsync()).HandleResponse();
    
}

