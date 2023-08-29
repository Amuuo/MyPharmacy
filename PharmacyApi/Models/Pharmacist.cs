using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PharmacyApi.Models;

public partial class Pharmacist
{
    public int Id { get; set; }

    public int PharmacyId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int Age { get; set; }

    public DateTime HireDate { get; set; }

    public string PrimaryRx { get; set; } = null!;

    [JsonIgnore]
    public virtual Pharmacy Pharmacy { get; set; } = null!;
}
