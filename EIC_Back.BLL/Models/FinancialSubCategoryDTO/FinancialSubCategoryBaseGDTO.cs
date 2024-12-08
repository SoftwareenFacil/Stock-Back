using System.ComponentModel.DataAnnotations;

namespace EIC_Back.BLL.Models.FinancialSubCategoryModelDTO
{
    /// <summary>
    /// Base class for FinancialSubCategory Data Transfer Objects (DTOs).
    /// </summary>
    public class FinancialSubCategoryBaseGDTO
    {
        /// <summary>
        /// Unique identifier of the FinancialSubCategory.
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Name of the FinancialSubCategory.
        /// </summary>
        [Required]
        public string? Name { get; set; }

        /// <summary>
        /// Description of the FinancialSubCategory.
        /// </summary>
        [Required]
        public string? Description { get; set; }

        public int? FinancialCategoryId { get; set; }
    }
}
