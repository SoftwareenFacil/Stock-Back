using System.ComponentModel.DataAnnotations;

namespace Stock_Back.BLL.Models.FinancialCategoryModelDTO
{
    /// <summary>
    /// Base class for Category Data Transfer Objects (DTOs) containing only the ID.
    /// </summary>
    public class FinancialCategoryBaseDTO
    {
        /// <summary>
        /// Unique identifier of the Category.
        /// </summary>
        [Required]
        public int Id { get; set; }
    }
}
