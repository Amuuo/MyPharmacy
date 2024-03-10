using MyPharmacy.Core.Utilities;
using MyPharmacy.Core.Utilities.Interfaces;
using MyPharmacy.Data.Entities;

namespace MyPharmacy.Services.Interfaces;

/// <summary>
/// Represents a service for managing pharmacists.
/// </summary>
public interface IPharmacistService
{
    /// <summary>
    /// Retrieves a paged list of pharmacists.
    /// </summary>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the paged list of pharmacists.</returns>
    Task<IServiceResult<IPagedResult<Pharmacist>?>> GetPagedPharmacistListAsync(PagingInfo pagingInfo);

    /// <summary>
    /// Retrieves a pharmacist by ID.
    /// </summary>
    /// <param name="id">The ID of the pharmacist.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the pharmacist.</returns>
    Task<IServiceResult<Pharmacist?>> GetPharmacistByIdAsync(int id);

    /// <summary>
    /// Retrieves a list of pharmacists by pharmacy ID.
    /// </summary>
    /// <param name="pharmacyId">The ID of the pharmacy.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of pharmacists.</returns>
    Task<IServiceResult<IEnumerable<Pharmacist>?>> GetPharmacistListByPharmacyIdAsync(int pharmacyId);

    /// <summary>
    /// Updates a pharmacist.
    /// </summary>
    /// <param name="pharmacistToUpdate">The pharmacist to update.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated pharmacist.</returns>
    Task<IServiceResult<Pharmacist?>> UpdatePharmacistAsync(Pharmacist pharmacist);

    /// <summary>
    /// Adds a new pharmacist.
    /// </summary>
    /// <param name="pharmacist">The pharmacist to add.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the added pharmacist.</returns>
    Task<IServiceResult<Pharmacist?>> AddPharmacistAsync(Pharmacist pharmacist);
}
