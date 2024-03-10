namespace MyPharmacy.Data.Entities;

public class Warehouse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Zip { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string? ModifiedBy { get; set; }
}

//public record Warehouse(
//    int Id,
//    string? Name,
//    string? Address,
//    string? City,
//    string? State,
//    string? Zip,
//    DateTime CreatedDate,
//    DateTime? ModifiedDate,
//    string? ModifiedBy
//);
