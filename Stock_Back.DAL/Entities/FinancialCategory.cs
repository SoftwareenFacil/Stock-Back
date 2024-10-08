using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock_Back.DAL.Models
{
    [Table("FinancialCategory")]
    public class FinancialCategory
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Type { get; set; } //Ingreso o Egreso
        [Required]
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public virtual ICollection<FinancialSubCategory> FinancialSubCategory { get; set; }
    }
}
