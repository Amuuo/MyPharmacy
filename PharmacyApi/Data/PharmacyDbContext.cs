using Microsoft.EntityFrameworkCore;
using PharmacyApi.Models;

namespace PharmacyApi.Data;

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
        modelBuilder.Entity<Delivery>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("delivery");

            entity.Property(e => e.DeliveryDate)
                .HasColumnType("date")
                .HasColumnName("delivery_date");
            entity.Property(e => e.DrugName)
                .HasMaxLength(100)
                .HasColumnName("drug_name");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.PharmacyId).HasColumnName("pharmacy_id");
            entity.Property(e => e.TotalPrice)
                .HasComputedColumnSql("([unit_count]*[unit_price])", false)
                .HasColumnType("money")
                .HasColumnName("total_price");
            entity.Property(e => e.UnitCount).HasColumnName("unit_count");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("money")
                .HasColumnName("unit_price");
            entity.Property(e => e.WarehouseId).HasColumnName("warehouse_id");

            entity.HasOne(d => d.Pharmacy).WithMany()
                .HasForeignKey(d => d.PharmacyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_delivery_pharmacy");

            entity.HasOne(d => d.Warehouse).WithMany()
                .HasForeignKey(d => d.WarehouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_delivery_warehouse");
        });

        modelBuilder.Entity<Pharmacist>(entity =>
        {
            entity.ToTable("pharmacist");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.HireDate)
                .HasColumnType("date")
                .HasColumnName("hire_date");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.PrimaryRx)
                .HasMaxLength(50)
                .HasColumnName("primary_rx");
        });

        modelBuilder.Entity<Pharmacy>(entity =>
        {
            entity.ToTable("pharmacy");

            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PrescriptionsFilled).HasColumnName("prescriptions_filled");
            entity.Property(e => e.State).HasMaxLength(2);
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
            entity.Property(e => e.Zip).HasMaxLength(20);
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.ToTable("warehouse");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.State)
                .HasMaxLength(2)
                .HasColumnName("state");
            entity.Property(e => e.Zip)
                .HasMaxLength(20)
                .HasColumnName("zip");
        });

        modelBuilder.Entity<PharmacyPharmacist>(entity =>
        {
            entity.HasKey(e => new { e.PharmacistId, e.PharmacyId });

            entity.ToTable("pharmacy_pharmacist");

            entity.Property(e => e.PharmacistId).HasColumnName("pharmacist_id");
            entity.Property(e => e.PharmacyId).HasColumnName("pharmacy_id");
        });


                modelBuilder.Entity<VwDeliveryDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_delivery_detail");

            entity.Property(e => e.DeliveryDate)
                .HasColumnType("date")
                .HasColumnName("delivery_date");
            entity.Property(e => e.DrugName)
                .HasMaxLength(100)
                .HasColumnName("drug_name");
            entity.Property(e => e.PharmacyTo)
                .HasMaxLength(50)
                .HasColumnName("pharmacy_to");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("money")
                .HasColumnName("total_price");
            entity.Property(e => e.UnitCount).HasColumnName("unit_count");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("money")
                .HasColumnName("unit_price");
            entity.Property(e => e.WarehouseFrom)
                .HasMaxLength(100)
                .HasColumnName("warehouse_from");
        });

        modelBuilder.Entity<VwPharmacistSalesSummary>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_pharmacist_sales_summary");

            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.Pharmacy)
                .HasMaxLength(50)
                .HasColumnName("pharmacy");
            entity.Property(e => e.PrimaryRx)
                .HasMaxLength(50)
                .HasColumnName("primary_rx");
            entity.Property(e => e.TotalNonPrimaryUnits).HasColumnName("total_non_primary_units");
            entity.Property(e => e.TotalPrimaryUnits).HasColumnName("total_primary_units");
        });

        modelBuilder.Entity<VwWarehouseProfit>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_warehouse_profit");

            entity.Property(e => e.AverageProfitPerUnit)
                .HasColumnType("money")
                .HasColumnName("average_profit_per_unit");
            entity.Property(e => e.TotalDeliveryRevenue)
                .HasColumnType("money")
                .HasColumnName("total_delivery_revenue");
            entity.Property(e => e.TotalUnitCount).HasColumnName("total_unit_count");
            entity.Property(e => e.Warehouse)
                .HasMaxLength(100)
                .HasColumnName("warehouse");
        });
    }
}
