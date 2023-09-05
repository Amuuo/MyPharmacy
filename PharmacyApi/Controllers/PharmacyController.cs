using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Models;
using PharmacyApi.Services.Interfaces;
using PharmacyApi.Utilities;
using PharmacyApi.Utilities.Helpers;

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
    [Route("{id}")]
    public async Task<IActionResult> GetPharmacyById(int id)
    {
        var pharmacyResult = await _pharmacyService.GetPharmacyByIdAsync(id);

        return ControllerHelper.HandleResponse(pharmacyResult);
    }


    [HttpPost]
    [Route("search")]
    public async Task<IActionResult> SearchPharmacyList(PharmacyPagedSearch? searchCriteria)
    {
        searchCriteria ??= new PharmacyPagedSearch();
        
        var searchResult = await _pharmacyService.SearchPharmacyAsync(searchCriteria);
        
        return ControllerHelper.HandleResponse(searchResult);
    }
    

    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> UpdatePharmacy(Pharmacy pharmacy)
    {
        var updatedPharmacyResult = await _pharmacyService.UpdatePharmacyAsync(pharmacy);

        return ControllerHelper.HandleResponse(updatedPharmacyResult);
    }


    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddPharmacy(Pharmacy newPharmacy)
    {
        var result = await _pharmacyService.InsertPharmacyAsync(newPharmacy);
    
        return ControllerHelper.HandleResponse(result);
    }

}
