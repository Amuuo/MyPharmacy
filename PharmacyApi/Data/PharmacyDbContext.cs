using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PharmacyApi.Models;

namespace PharmacyApi
{
    public class PharmacyDbContext : DbContext
    {
        public PharmacyDbContext(DbContextOptions options) : base(options)
        {

        }

        private DbSet<Pharmacy> Pharmacies { get; set; }
    }
}