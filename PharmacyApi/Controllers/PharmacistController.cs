using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Services.Interfaces;
using PharmacyApi.Utilities.Helpers;

namespace PharmacyApi.Controllers;

[ApiController]
[Route("pharmacist")]
public class PharmacistController : ControllerBase
{
    #region Members and Constructor

    private readonly ILogger<PharmacistController> _logger;
    private readonly IPharmacistService _pharmacistService;

    public PharmacistController(ILogger<PharmacistController> logger,
                                IPharmacistService pharmacistService)
    {
        _logger            = logger;
        _pharmacistService = pharmacistService;
    }

    #endregion

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> GetPharmacistList()
    {
        return (await _pharmacistService.GetPharmacistListAsync()).HandleResponse();
    }


    [HttpGet]
    [Route("{pharmacyId}")]
    public async Task<IActionResult> GetPharmacistListByPharmacyId(int pharmacyId)
    {
        return (await _pharmacistService.GetPharmacistListByPharmacyIdAsync(pharmacyId)).HandleResponse();
    }
}

