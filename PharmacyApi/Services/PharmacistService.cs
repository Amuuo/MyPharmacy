using System.Net;
using Microsoft.EntityFrameworkCore;
using PharmacyApi.Data;
using PharmacyApi.Models;
using PharmacyApi.Services.Interfaces;
using PharmacyApi.Utilities;

namespace PharmacyApi.Services
{
    public class PharmacistService : IPharmacistService
    {
        private readonly ILogger _logger;
        private readonly IPharmacyDbContext _dbContext;

        public PharmacistService(ILogger<IPharmacistService> logger,
                                 IPharmacyDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        public async Task<ServiceResult<IAsyncEnumerable<Pharmacist>>> GetPharmacistList()
        {
            var pharmacists = _dbContext.PharmacistList
                .Include(p => p.Pharmacy)
                .AsAsyncEnumerable();

            var hasPharmacists = await pharmacists.AnyAsync();

            if (hasPharmacists)
            {
                return new ServiceResult<IAsyncEnumerable<Pharmacist>>
                {
                    IsSuccess  = true,
                    StatusCode = HttpStatusCode.OK,
                    Result     = pharmacists
                };
            }

            return new ServiceResult<IAsyncEnumerable<Pharmacist>>
            {
                IsSuccess    = false,
                StatusCode   = HttpStatusCode.NoContent,
                ErrorMessage = "No pharmacists found"
            };
        }
    }
}
