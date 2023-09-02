using System.Text.Json.Serialization;

namespace PharmacyApi.Models;

public class Pharmacist
{
    public int      Id        { get; set; }
    public string   FirstName { get; set; } = null!;
    public string   LastName  { get; set; } = null!;
    public int      Age       { get; set; }
    public DateTime HireDate  { get; set; }
    public string   PrimaryRx { get; set; } = null!;

    [JsonIgnore]
    public ICollection<PharmacyPharmacist>? PharmacyPharmacists { get; set; }
}