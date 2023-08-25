using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PharmacyApi.Models;

namespace PharmacyApi.Data;

public class PharmacyDbContext : DbContext, IPharmacyDbContext
{
    private readonly ILogger _logger;

    public PharmacyDbContext(DbContextOptions options, 
                             ILogger<PharmacyDbContext> logger) : base(options)
    {
        _logger = logger;
    }

    public DbSet<Pharmacy> PharmacyList { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pharmacy>()
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();

        base.OnModelCreating(modelBuilder);
    }

}
