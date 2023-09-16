﻿using PharmacyApi.Models;
using PharmacyApi.Utilities.Interfaces;

namespace PharmacyApi.Services.Interfaces;

public interface IPharmacistService
{
    Task<IServiceResult<IAsyncEnumerable<Pharmacist>>> GetPharmacistListAsync();
    Task<IServiceResult<Pharmacist>> GetPharmacistByIdAsync(int id);
    Task<IServiceResult<IAsyncEnumerable<Pharmacist>>> GetPharmacistListByPharmacyIdAsync(int pharmacyId);
    Task<IServiceResult<Pharmacist>> UpdatePharmacistAsync(Pharmacist pharmacist);
    Task<IServiceResult<Pharmacist>> AddPharmacistAsync(Pharmacist pharmacist);
}
