namespace PharmacyApi.Models;

public partial class VwPharmacistSalesSummary
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Pharmacy { get; set; } = null!;

    public string PrimaryRx { get; set; } = null!;

    public int? TotalPrimaryUnits { get; set; }

    public int? TotalNonPrimaryUnits { get; set; }
}
