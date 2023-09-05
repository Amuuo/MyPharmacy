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
    public async Task<IActionResult> GetPharmacyById(int id) =>
        (await _pharmacyService.GetPharmacyByIdAsync(id)).HandleResponse();
    
    [HttpGet]
    [Route("by-pharmacist/{id}")]
    public async Task<IActionResult> GetPharmaciesByPharmacistId(int id) =>
        (await _pharmacyService.GetPharmaciesByPharmacistIdAsync(id)).HandleResponse();

    [HttpPost]
    [Route("search/paged")]
    public async Task<IActionResult> SearchPagedPharmacyList(PharmacyPagedSearch? searchCriteria)
    {
        searchCriteria ??= new PharmacyPagedSearch();
        
        return (await _pharmacyService.SearchPharmacyListPagedAsync(searchCriteria)).HandleResponse();
    }
    
    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> UpdatePharmacy(Pharmacy updatedPharmacy) =>
        (await _pharmacyService.UpdatePharmacyAsync(updatedPharmacy)).HandleResponse();
    

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddPharmacy(Pharmacy newPharmacy) =>
        (await _pharmacyService.InsertPharmacyAsync(newPharmacy)).HandleResponse();
    
}
