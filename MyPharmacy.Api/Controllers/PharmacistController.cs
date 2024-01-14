using Microsoft.AspNetCore.Mvc;
using MyPharmacy.Core.Helpers;
using MyPharmacy.Core.Utilities;
using MyPharmacy.Data.Entities;
using MyPharmacy.Services.Interfaces;

namespace PharmacyApi.Controllers;

[ApiController]
[Route("pharmacist")]
public class PharmacistController(
    ILogger<PharmacistController> logger, 
    IPharmacistService pharmacistService) : ControllerBase
{
    private readonly ILogger<PharmacistController> _logger = logger;

    [HttpGet]
    public async Task<IActionResult> GetPagedPharmacistList([FromQuery]PagingInfo pagingInfo) => 
        (await pharmacistService.GetPagedPharmacistListAsync(pagingInfo)).HandleResponse();

    [HttpGet("by-pharmacy/{id}")]
    public async Task<IActionResult> GetPharmacistListByPharmacyId(int id) =>
        (await pharmacistService.GetPharmacistListByPharmacyIdAsync(id)).HandleResponse();

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPharmacistById(int id) =>
        (await pharmacistService.GetPharmacistByIdAsync(id)).HandleResponse();

    [HttpPut("update")]
    public async Task<IActionResult> UpdatePharmacist(Pharmacist pharmacist) =>
        (await pharmacistService.UpdatePharmacistAsync(pharmacist)).HandleResponse();

    [HttpPost("add")]
    public async Task<IActionResult> AddPharmacist(Pharmacist pharmacist) =>
        (await pharmacistService.AddPharmacistAsync(pharmacist)).HandleResponse();

}

