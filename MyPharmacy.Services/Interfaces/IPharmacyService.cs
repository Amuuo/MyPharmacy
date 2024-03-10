using MyPharmacy.Core.Utilities;
using MyPharmacy.Core.Utilities.Interfaces;
using MyPharmacy.Data.Entities;

namespace MyPharmacy.Services.Interfaces;

public interface IPharmacyService
{
    /// <summary>
    /// Asynchronously retrieves pharmacy records based on the search criteria.
    /// </summary>
    /// <param name="pagedSearch">The search criteria object.</param>
    /// <param name="pagingInfo"></param>
    /// <returns>
    /// A ServiceResult containing IAsyncEnumerable of Pharmacy objects if any match the search criteria,
    /// or an error message if no matching pharmacies are found or if an exception occurs during retrieval.
    /// </returns>
    Task<IServiceResult<IPagedResult<Pharmacy>?>> GetPharmacyListPagedAsync(PagingInfo pagingInfo);
    
    /// <summary>
    /// Asynchronously updates a pharmacy record by its unique identifier with the provided details.
    /// </summary>
    /// <param name="updatedPharmacy">The updated pharmacy object containing the new details.</param>
    /// <returns>
    /// A ServiceResult containing the updated Pharmacy object if successful,
    /// or an error message if no pharmacy is found with the given id or if an exception occurs during the update.
    /// </returns>
    Task<IServiceResult<Pharmacy?>> UpdatePharmacyAsync(Pharmacy updatedPharmacy);
    
    /// <summary>
    /// Asynchronously retrieves a pharmacy record by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the pharmacy record to retrieve.</param>
    /// <returns>
    /// A ServiceResult containing the Pharmacy object if found, 
    /// or an error message if no pharmacy record with the specified id exists.
    /// </returns>
    Task<IServiceResult<Pharmacy?>> GetPharmacyByIdAsync(int id);
    
    /// <summary>
    /// Asynchronously inserts a new pharmacy record.
    /// </summary>
    /// <param name="newPharmacy">The new pharmacy object to insert.</param>
    /// <returns>
    /// A ServiceResult containing the inserted Pharmacy object if successful,
    /// or an error message if an exception occurs during the insertion.
    /// </returns>
    Task<IServiceResult<Pharmacy?>> InsertPharmacyAsync(Pharmacy? newPharmacy);
    
    /// <summary>
    /// Asynchronously retrieves pharmacies associated with a specific pharmacist using the relationship table.
    /// </summary>
    /// <param name="pharmacistId">The unique identifier of the pharmacist.</param>
    /// <returns> 
    /// A ServiceResult containing IAsyncEnumerable of Pharmacy objects associated with the specified pharmacist,
    /// or an error message if no pharmacies are found or if an exception occurs during retrieval.
    /// </returns>
    Task<IServiceResult<IEnumerable<Pharmacy>?>> GetPharmaciesByPharmacistIdAsync(int pharmacistId);
}

