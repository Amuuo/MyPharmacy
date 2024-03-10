using Microsoft.AspNetCore.Mvc;
using MyPharmacy.Core.Helpers;
using MyPharmacy.Core.Utilities;
using MyPharmacy.Data.Entities;
using MyPharmacy.Services.Interfaces;

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
    public async Task<IActionResult> GetPagedPharmacyList([FromQuery]PagingInfo pagingInfo) =>
        (await _pharmacyService.GetPharmacyListPagedAsync(pagingInfo)).HandleResponse();

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPharmacyById(int id) =>
        (await _pharmacyService.GetPharmacyByIdAsync(id)).HandleResponse();

    [HttpGet("by-pharmacist/{id}")]
    public async Task<IActionResult> GetPharmaciesByPharmacistId(int id) =>
        (await _pharmacyService.GetPharmaciesByPharmacistIdAsync(id)).HandleResponse();

    [HttpPut("update")]
    public async Task<IActionResult> UpdatePharmacy(Pharmacy updatedPharmacy) =>
        (await _pharmacyService.UpdatePharmacyAsync(updatedPharmacy)).HandleResponse();
    
    [HttpPost("add")]
    public async Task<IActionResult> AddPharmacy(Pharmacy newPharmacy) =>
        (await _pharmacyService.InsertPharmacyAsync(newPharmacy)).HandleResponse();

}
