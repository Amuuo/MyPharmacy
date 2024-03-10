using MyPharmacy.Core.Utilities;
using MyPharmacy.Core.Utilities.Interfaces;
using MyPharmacy.Data.Entities;

namespace MyPharmacy.Data.Repository.Interfaces;

public interface IPharmacyRepository
{
    Task<IPagedResult<Pharmacy>?> GetPharmacyListPagedAsync(PagingInfo pagingInfo); 
    Task<Pharmacy?> GetByIdAsync(int id);
    Task<Pharmacy?> InsertPharmacyAsync(Pharmacy pharmacy);
    Task<Pharmacy?> UpdatePharmacyAsync(Pharmacy pharmacy);
}