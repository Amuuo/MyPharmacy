namespace MyPharmacy.Data.Entities;

public partial class VwPharmacistSalesSummary
{
    public int PharmacistId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string PharmacyList { get; set; } = null!;

    public string PrimaryRx { get; set; } = null!;

    public int? TotalPrimaryUnits { get; set; }

    public int? TotalNonPrimaryUnits { get; set; }
}
