using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Models;
using PharmacyApi.Services;

namespace PharmacyApi.Controllers;

//[Authorize]
[ApiController]
[Route("pharmacy")]
//[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class PharmacyController : ControllerBase
{
    #region Members and Constructor

    private readonly ILogger<PharmacyController> _logger;
    private readonly IPharmacyService _pharmacyService;

    public PharmacyController(ILogger<PharmacyController> logger, 
                              IPharmacyService pharmacyService)
    {
        _logger = logger;
        _pharmacyService = pharmacyService;
    }

    #endregion

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAll()
    {
        var pharmacyListResult = await _pharmacyService.GetPharmacyListAsync();
        
        return pharmacyListResult.Success 
            ? Ok(pharmacyListResult.Result)
            : NotFound(pharmacyListResult.ErrorMessage);
    } 
    
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var pharmacyResult = await _pharmacyService.GetByIdAsync(id);

        return pharmacyResult.Success
            ? Ok(pharmacyResult.Result)
            : NotFound(pharmacyResult.ErrorMessage);
    }

    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> UpdateById(Pharmacy pharmacy)
    {
        var updatedPharmacyResult = await _pharmacyService.UpdateByIdAsync(pharmacy);

        return updatedPharmacyResult.Success
            ? Ok(updatedPharmacyResult.Result)
            : NotFound(updatedPharmacyResult.ErrorMessage);
    }
}
