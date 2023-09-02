namespace PharmacyApi.Models;

public class PharmacyPharmacist
{
    public int PharmacistId { get; set; }
    public Pharmacist Pharmacist { get; set; } = null!;

    public int PharmacyId { get; set; }
    public Pharmacy Pharmacy { get; set; } = null!;
}
