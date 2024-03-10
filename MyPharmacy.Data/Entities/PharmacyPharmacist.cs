namespace MyPharmacy.Data.Entities;

public class PharmacyPharmacist
{
    public int PharmacistId { get; init; }
    public virtual Pharmacist Pharmacist { get; init; }

    public int PharmacyId { get; init; }
    public virtual Pharmacy Pharmacy { get; init; }

    public DateTime? CreatedDate { get; init; }
    public DateTime? ModifiedDate { get; init; }
    public string? ModifiedBy { get; init; }
}
