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
    public IActionResult GetAll() => Ok(_pharmacyService.GetAll());
    
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var pharmacy = await _pharmacyService.GetById(id);

        return pharmacy is not null
            ? Ok(pharmacy)
            : StatusCode(404, $"No pharmacy found with id {id}");
    }

    [HttpPut]
    [Route("update/{id}")]
    public async Task<IActionResult> UpdateById(int id, Pharmacy pharmacy)
    {
        var updatedPharmacy = await _pharmacyService.UpdateById(id, pharmacy);

        return updatedPharmacy is not null
            ? Ok(updatedPharmacy)
            : StatusCode(404, $"No pharmacy found with id {id}");
    }
}
