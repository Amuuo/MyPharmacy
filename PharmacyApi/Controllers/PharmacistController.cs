using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Models;
using PharmacyApi.Services.Interfaces;
using PharmacyApi.Utilities;

namespace PharmacyApi.Controllers
{
    [ApiController]
    [Route("pharmacist")]
    public class PharmacistController : ControllerBase
    {
        private readonly ILogger<PharmacistController> _logger;
        private readonly IPharmacistService _pharmacistService;

        public PharmacistController(ILogger<PharmacistController> logger,
                                    IPharmacistService pharmacistService)
        {
            _logger            = logger;
            _pharmacistService = pharmacistService;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> GetPharmacistList()
        {
            var result = await _pharmacistService.GetPharmacistList();
            return ControllerHelper.HandleResponse(result);
        }
    }
}
