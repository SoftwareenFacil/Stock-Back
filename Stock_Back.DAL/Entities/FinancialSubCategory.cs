using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock_Back.DAL.Models
{
    [Table("FinancialSubCategory")]
    public class FinancialSubCategory
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        [Required]
        public int FinancialCategoryId { get; set; }
        [ForeignKey("FinancialCategoryId")]
        public FinancialCategory? FinancialCategory { get; set; }
        public virtual ICollection<FinancialMovements> FinancialMovements { get; set; }

    }
}