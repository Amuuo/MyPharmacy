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
    private readonly IReportService _reportService = reportService;
    private readonly ILogger<ReportingController> _logger = logger;

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

