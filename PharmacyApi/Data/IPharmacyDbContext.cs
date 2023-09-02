using Microsoft.EntityFrameworkCore;
using PharmacyApi.Models;

namespace PharmacyApi.Data
{
    public interface IPharmacyDbContext : IDisposable
    {
        DbSet<Pharmacy>   PharmacyList   { get; }
        DbSet<Delivery>   DeliveryList   { get; }
        DbSet<Pharmacist> PharmacistList { get; }
        DbSet<Warehouse>  WarehouseList  { get; }
        DbSet<PharmacyPharmacist> PharmacyPharmacists { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
