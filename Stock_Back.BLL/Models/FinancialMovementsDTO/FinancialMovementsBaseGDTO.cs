using System.ComponentModel.DataAnnotations;

namespace Stock_Back.BLL.Models.FinancialMovementsModelDTO
{
    /// <summary>
    /// Base class for FinancialMovements Data Transfer Objects (DTOs).
    /// </summary>
    public class FinancialMovementsBaseGDTO
    {
        /// <summary>
        /// Unique identifier of the FinancialMovements.
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Name of the FinancialMovements.
        /// </summary>
        [Required]
        public string? Name { get; set; }

        /// <summary>
        /// Description of the FinancialMovements.
        /// </summary>
        [Required]
        public string? Description { get; set; }
        public int? ReferenceId { get; set; }
        public int? Type { get; set; }
        public string? Bank { get; set; }
        public decimal Amout { get; set; }
        public int FinancialSubCategoryId { get; set; }
        public DateTime DocumentDate { get; set; }
    }
}
