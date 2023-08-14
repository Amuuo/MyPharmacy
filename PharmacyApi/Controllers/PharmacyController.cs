using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Models;
using PharmacyApi.Services;

namespace PharmacyApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class PharmacyController : ControllerBase
    {
        private readonly ILogger<PharmacyController> _logger;
        private readonly IPharmacyService _pharmacyService;

        public PharmacyController(ILogger<PharmacyController> logger, 
                                  IPharmacyService pharmacyService)
        {
            _logger = logger;
            _pharmacyService = pharmacyService;
        }

        [HttpGet]
        [Route("all")]
        public IEnumerable<Pharmacy> GetPharmacies() => _pharmacyService.GetPharmacies();


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPharmacyById(int id)
        {
            var pharmacy = await _pharmacyService.GetPharmacyById(id);

            return pharmacy is not null ? Ok(pharmacy) : StatusCode(404, $"No pharmacy found with id {id}");
        }

        [HttpPost]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdatePharmacyById(int id, Pharmacy pharmacy)
        {
            var updatedPharmacy = await _pharmacyService.UpdatePharmacyById(id, pharmacy);

            return updatedPharmacy is not null ? Ok(pharmacy) : StatusCode(404, $"No pharmacy found with id {id}");
        }
    }
}