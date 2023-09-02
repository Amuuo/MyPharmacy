using Microsoft.AspNetCore.Mvc;
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
            var result = await _pharmacistService.GetPharmacistListAsync();
            return ControllerHelper.HandleResponse(result);
        }


        [HttpGet]
        [Route("{pharmacyId}")]
        public async Task<IActionResult> GetPharmacistListByPharmacyId(int pharmacyId)
        {
            var pharmacistResult = await _pharmacistService.GetPharmacistListByPharmacyIdAsync(pharmacyId);

            return ControllerHelper.HandleResponse(pharmacistResult);
        }
    }
}
