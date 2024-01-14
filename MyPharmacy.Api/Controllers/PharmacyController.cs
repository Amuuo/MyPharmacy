using System.Runtime.CompilerServices;
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

    [HttpGet]
    public async Task<IActionResult> GetPagedPharmacyList([FromQuery]PagingInfo pagingInfo) =>
        (await pharmacyService.GetPharmacyListPagedAsync(pagingInfo)).HandleResponse();

    //[HttpGet]
    //[Route("streaming")]
    //public async Task<IActionResult> GetStreamedPagedPharmacyList(int pageNumber = 0, int pageSize = 10) =>
    //    (await _pharmacyService.GetPharmacyListPagedAsync(pageNumber, pageSize)).HandlePagedStreamingResponse();

    [HttpGet("streaming")]
    public IAsyncEnumerable<Pharmacy> GetStreamedPagedPharmacyList()
    {
        return pharmacyService.GetPharmacyListAsync();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPharmacyById(int id) =>
        (await pharmacyService.GetPharmacyByIdAsync(id)).HandleResponse();

    [HttpGet("by-pharmacist/{id}")]
    public async Task<IActionResult> GetPharmaciesByPharmacistId(int id) =>
        (await pharmacyService.GetPharmaciesByPharmacistIdAsync(id)).HandleResponse();

    [HttpPut("update")]
    public async Task<IActionResult> UpdatePharmacy(Pharmacy updatedPharmacy) =>
        (await pharmacyService.UpdatePharmacyAsync(updatedPharmacy)).HandleResponse();
    
    [HttpPost("add")]
    public async Task<IActionResult> AddPharmacy(Pharmacy newPharmacy) =>
        (await pharmacyService.InsertPharmacyAsync(newPharmacy)).HandleResponse();
    
}
