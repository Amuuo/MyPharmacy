using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Models;
using PharmacyApi.Services.Interfaces;
using PharmacyApi.Utilities;
using PharmacyApi.Utilities.Helpers;

namespace PharmacyApi.Controllers;

[ApiController]
[Route("pharmacist")]
public class PharmacistController : ControllerBase
{
    #region Members and Constructor

    private readonly ILogger<PharmacistController> _logger;
    private readonly IPharmacistService _pharmacistService;

    public PharmacistController(ILogger<PharmacistController> logger,
                                IPharmacistService pharmacistService)
    {
        _logger            = logger;
        _pharmacistService = pharmacistService;
    }

    #endregion

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetPagedPharmacistList(int pageNumber = 1, int pageSize = 10) => 
        (await _pharmacistService.GetPagedPharmacistListAsync(pageNumber, pageSize)).HandleResponse();

    [HttpGet]
    [Route("by-pharmacy/{id}")]
    public async Task<IActionResult> GetPharmacistListByPharmacyId(int id) =>
        (await _pharmacistService.GetPharmacistListByPharmacyIdAsync(id)).HandleResponse();

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetPharmacistById(int id) =>
        (await _pharmacistService.GetPharmacistByIdAsync(id)).HandleResponse();

    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> UpdatePharmacist(Pharmacist pharmacist) =>
        (await _pharmacistService.UpdatePharmacistAsync(pharmacist)).HandleResponse();

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddPharmacist(Pharmacist pharmacist) =>
        (await _pharmacistService.AddPharmacistAsync(pharmacist)).HandleResponse();

}

