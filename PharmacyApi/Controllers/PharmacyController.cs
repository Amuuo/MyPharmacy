using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Models;
using PharmacyApi.Services.Interfaces;
using PharmacyApi.Utilities;
using PharmacyApi.Utilities.Helpers;

namespace PharmacyApi.Controllers;

[ApiController]
[Route("pharmacy")]
public class PharmacyController(
    ILogger<PharmacyController> logger, 
    IPharmacyService pharmacyService) : ControllerBase
{
    private readonly ILogger<PharmacyController> _logger = logger;
    private readonly IPharmacyService _pharmacyService = pharmacyService;

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetPagedPharmacyList(int pageNumber = 1, int pageSize = 10) =>
        (await _pharmacyService.GetPharmacyListPagedAsync(pageNumber, pageSize)).HandleResponse();
    
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetPharmacyById(int id) =>
        (await _pharmacyService.GetPharmacyByIdAsync(id)).HandleResponse();
    
    [HttpGet]
    [Route("by-pharmacist/{id}")]
    public async Task<IActionResult> GetPharmaciesByPharmacistId(int id) =>
        (await _pharmacyService.GetPharmaciesByPharmacistIdAsync(id)).HandleResponse();

    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> UpdatePharmacy(Pharmacy updatedPharmacy) =>
        (await _pharmacyService.UpdatePharmacyAsync(updatedPharmacy)).HandleResponse();
    
    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddPharmacy(Pharmacy newPharmacy) =>
        (await _pharmacyService.InsertPharmacyAsync(newPharmacy)).HandleResponse();
    
}
