using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PharmacyApi.Models;

namespace PharmacyApi.Data;

public class PharmacyDbContext : DbContext, IPharmacyDbContext
{

    public PharmacyDbContext(DbContextOptions options) : base(options)
    {
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
