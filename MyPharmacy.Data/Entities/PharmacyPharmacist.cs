namespace MyPharmacy.Data.Entities;

public class PharmacyPharmacist
{
    public int PharmacyPharmacistId { get; init; }

    public int PharmacistId { get; init; }
    public virtual Pharmacist Pharmacist { get; init; }

    public int PharmacyId { get; init; }
    public virtual Pharmacy Pharmacy { get; init; }
}
