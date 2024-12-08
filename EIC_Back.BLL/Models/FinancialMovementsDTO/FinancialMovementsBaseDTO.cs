using System.ComponentModel.DataAnnotations;

namespace EIC_Back.BLL.Models.FinancialMovementsModelDTO
{
    /// <summary>
    /// Base class for FinancialMovements Data Transfer Objects (DTOs) containing only the ID.
    /// </summary>
    public class FinancialMovementsBaseDTO
    {
        /// <summary>
        /// Unique identifier of the FinancialMovements.
        /// </summary>
        [Required]
        public int Id { get; set; }
    }
}
