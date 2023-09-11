using Microsoft.EntityFrameworkCore;
using PharmacyApi.Models;

namespace PharmacyApi.Data
{
    public interface IPharmacyDbContext : IDisposable
    {
        DbSet<Pharmacy>                 PharmacyList               { get; }
        DbSet<Delivery>                 DeliveryList               { get; }
        DbSet<Pharmacist>               PharmacistList             { get; }
        DbSet<Warehouse>                WarehouseList              { get; }
        DbSet<PharmacyPharmacist>       PharmacyPharmacists        { get; }
        DbSet<VwDeliveryDetail>         VwDeliveryDetails          { get; }
        DbSet<VwPharmacistSalesSummary> VwPharmacistSalesSummaries { get; }
        DbSet<VwWarehouseProfit>        VwWarehouseProfits         { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
