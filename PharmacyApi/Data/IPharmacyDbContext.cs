using Microsoft.EntityFrameworkCore;
using PharmacyApi.Models;

namespace PharmacyApi.Data
{
    public interface IPharmacyDbContext : IDisposable
    {
        DbSet<Pharmacy> PharmacyList { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
