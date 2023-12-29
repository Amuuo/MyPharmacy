using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace MyPharmacy.Data.Models;

public class PharmacyPharmacist
{
    public int PharmacyPharmacistId { get; set; }

    public int PharmacistId { get; set; }
    public virtual Pharmacist Pharmacist { get; init; }

    public int PharmacyId { get; set; }
    public virtual Pharmacy Pharmacy { get; init; }
}
