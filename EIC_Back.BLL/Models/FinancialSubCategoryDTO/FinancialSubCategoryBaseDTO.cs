using System.ComponentModel.DataAnnotations;

namespace EIC_Back.BLL.Models.FinancialSubCategoryModelDTO
{
    /// <summary>
    /// Base class for FinancialSubCategory Data Transfer Objects (DTOs) containing only the ID.
    /// </summary>
    public class FinancialSubCategoryBaseDTO
    {
        /// <summary>
        /// Unique identifier of the FinancialSubCategory.
        /// </summary>
        [Required]
        public int Id { get; set; }
    }
}
