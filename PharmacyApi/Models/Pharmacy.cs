using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyApi.Models
{
    [Table("pharmacy")]
    public class Pharmacy
    {
        public int? Id { get; set; }
        
        [StringLength(50)]  public string? Name    { get; set; }
        [StringLength(100)] public string? Address { get; set; }
        [StringLength(50)]  public string? City    { get; set; }
        [StringLength(2)]   public string? State   { get; set; }
        [StringLength(20)]  public string? Zip     { get; set; }
        
        public int? PrescriptionsFilled { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}