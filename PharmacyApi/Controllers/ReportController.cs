using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Services.Interfaces;
using PharmacyApi.Utilities.Helpers;

namespace PharmacyApi.Controllers
{
    [ApiController]
    [Route("report")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly ILogger<ReportController> _logger;

        public ReportController(ILogger<ReportController> logger, IReportService reportService)
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
}
