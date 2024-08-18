using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock_Back.DAL.Models
{
    [Table("MaterialType")]
    public class MaterialType
    {
        [Key, Required]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? MetricUnit { get; set; }
        public string? Vendor { get; set; }
        public string? VendorUnitType { get; set; }
        public double CostPerUnit { get; set; }
        public string? ProviderName { get; set; }
        public int UnitsPerSinglePurchase { get; set; }
        public string? PricingPerSinglePurchase { get; set; }
        [Required]
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
