using Microsoft.EntityFrameworkCore;
using PharmacyApi.Models;

namespace PharmacyApi.Data
{
    public interface IPharmacyDbContext
    {
        DbSet<Pharmacy> PharmacyList { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
