using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Models;
using PharmacyApi.Services.Interfaces;
using PharmacyApi.Utilities;

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

    [HttpPost]
    [Route("search")]
    public async Task<IActionResult> SearchPharmacyList(PharmacySearch? searchCriteria)
    {
        searchCriteria ??= new PharmacySearch();
        
        var searchResult = await _pharmacyService.SearchPharmacyAsync(searchCriteria);
        
        return ControllerHelper.HandleResponse(searchResult);
    }
    

    [HttpPost]
    [Route("update")]
    public async Task<IActionResult> UpdatePharmacy(Pharmacy pharmacy)
    {
        var updatedPharmacyResult = await _pharmacyService.UpdatePharmacyAsync(pharmacy);

        return ControllerHelper.HandleResponse(updatedPharmacyResult);
    }
}
