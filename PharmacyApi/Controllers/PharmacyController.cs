using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Models;
using PharmacyApi.Services.Interfaces;
using PharmacyApi.Utilities;
using PharmacyApi.Utilities.Helpers;

namespace PharmacyApi.Controllers;

[ApiController]
[Route("pharmacy")]
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
        return (await _pharmacyService.GetPharmacyByIdAsync(id)).HandleResponse();
    }

    [HttpPost]
    [Route("search")]
    public async Task<IActionResult> SearchPharmacyList(PharmacyPagedSearch? searchCriteria)
    {
        searchCriteria ??= new PharmacyPagedSearch();
        
        return (await _pharmacyService.SearchPharmacyListAsync(searchCriteria)).HandleResponse();
    }
    
    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> UpdatePharmacy(Pharmacy updatedPharmacy)
    {
        return (await _pharmacyService.UpdatePharmacyAsync(updatedPharmacy)).HandleResponse();
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddPharmacy(Pharmacy newPharmacy)
    {
        return (await _pharmacyService.InsertPharmacyAsync(newPharmacy)).HandleResponse();
    }

}
