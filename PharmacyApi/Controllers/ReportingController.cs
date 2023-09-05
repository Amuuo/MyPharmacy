using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Services.Interfaces;
using PharmacyApi.Utilities.Helpers;

namespace PharmacyApi.Controllers;

[ApiController]
[Route("reporting")]
public class ReportingController : ControllerBase
{
    private readonly IReportService _reportService;
    private readonly ILogger<ReportingController> _logger;

    public ReportingController(ILogger<ReportingController> logger, IReportService reportService)
    {
        _logger = logger;
        _reportService = reportService;
    }

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

