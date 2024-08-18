using System.ComponentModel.DataAnnotations;

namespace Stock_Back.BLL.Models.FinancialCategoryModelDTO
{
    /// <summary>
    /// Base class for Category Data Transfer Objects (DTOs).
    /// </summary>
    public class FinancialCategoryBaseGDTO
    {
        /// <summary>
        /// Unique identifier of the Category.
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Name of the Category.
        /// </summary>
        [Required]
        public string? Name { get; set; }

        /// <summary>
        /// Description of the Category.
        /// </summary>
        [Required]
        public string? Description { get; set; }
        /// <summary>
        /// Movement type  I = Income / E = Expense
        /// </summary>
        [Required]
        public string? Type { get; set; } //Ingreso o Egreso
    }
}
