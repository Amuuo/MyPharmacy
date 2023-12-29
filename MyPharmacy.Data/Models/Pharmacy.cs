using System.Text.Json.Serialization;

namespace MyPharmacy.Data.Models;

public class Pharmacy
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Zip { get; set; }

    public int? PrescriptionsFilled { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<PharmacyPharmacist>? PharmacyPharmacists { get; set; }
}
