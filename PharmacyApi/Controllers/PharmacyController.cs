using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using PharmacyApi.Models;
using PharmacyApi.Services;

namespace PharmacyApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
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

        //[HttpGet(Name = "GetPharmacies")]
        [HttpGet]
        [Route("all")]
        public IEnumerable<Pharmacy> GetPharmacies()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{id}")]
        public Pharmacy GetPharmacyById(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("update/{id}")]
        public void UpdatePharmacyById(int id, Pharmacy pharmacy)
        {
            throw new NotImplementedException();
        }
        
    }
}