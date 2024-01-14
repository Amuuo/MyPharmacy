using System.Text.Json.Serialization;

namespace MyPharmacy.Data.Entities;

public class Pharmacist
{
    public int       Id        { get; set; }
    public string?   FirstName { get; set; } 
    public string?   LastName  { get; set; } 
    public int?      Age       { get; set; }
    public DateTime? HireDate  { get; set; }
    public string?   PrimaryRx { get; set; }

    [JsonIgnore]
    public virtual ICollection<PharmacyPharmacist>? PharmacyPharmacists { get; set; }
}