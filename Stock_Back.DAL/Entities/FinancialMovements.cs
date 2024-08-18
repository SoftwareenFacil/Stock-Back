using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock_Back.DAL.Models
{
    [Table("FinancialMovements")]
    public class FinancialMovements
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        public int? ReferenceId { get; set; }//id Projecy
        public int? Type { get; set; }//Proyecto
        public string? Bank { get; set; }
        public decimal Amout { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime DocumentDate { get; set; }
        public DateTime Updated { get; set; }
        [Required]
        public int FinancialSubCategoryId { get; set; }
        [ForeignKey("FinancialSubCategoryId")]
        public FinancialSubCategory? FinancialSubCategory { get; set; }
    }
}