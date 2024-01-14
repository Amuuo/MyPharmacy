using Microsoft.EntityFrameworkCore;
using MyPharmacy.Data.Entities;

namespace MyPharmacy.Data;

public class PharmacyDbContext : DbContext, IPharmacyDbContext
{

    public PharmacyDbContext(DbContextOptions options) : base(options)
    {
    }

    public virtual DbSet<Pharmacy>   PharmacyList   { get; set; }
    public virtual DbSet<Delivery>   DeliveryList   { get; set; }
    public virtual DbSet<Pharmacist> PharmacistList { get; set; }
    public virtual DbSet<Warehouse>  WarehouseList  { get; set; }
    public virtual DbSet<PharmacyPharmacist>       PharmacyPharmacists        { get; set; }
    public virtual DbSet<VwDeliveryDetail>         VwDeliveryDetails          { get; set; }
    public virtual DbSet<VwPharmacistSalesSummary> VwPharmacistSalesSummaries { get; set; }
    public virtual DbSet<VwWarehouseProfit>        VwWarehouseProfits         { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<PharmacyPharmacist>()
        //    .HasKey(pp => new { pp.PharmacistId, pp.PharmacyId });

        modelBuilder.Entity<PharmacyPharmacist>()
            .HasOne(pp => pp.Pharmacist)
            .WithMany(p => p.PharmacyPharmacists)
            .HasForeignKey(pp => pp.PharmacistId);

        modelBuilder.Entity<PharmacyPharmacist>()
            .HasOne(pp => pp.Pharmacy)
            .WithMany(p => p.PharmacyPharmacists)
            .HasForeignKey(pp => pp.PharmacyId);


        modelBuilder.Entity<VwDeliveryDetail>(entity =>
        {
            entity.HasNoKey();
        });
        modelBuilder.Entity<VwPharmacistSalesSummary>(entity =>
        {
            entity.HasNoKey();
        });
        modelBuilder.Entity<VwWarehouseProfit>(entity =>
        {
            entity.HasNoKey();
        });

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            modelBuilder.Entity(entityType.Name).ToTable(entityType.ClrType.Name);
        }
    }
}
