using System.ComponentModel.DataAnnotations;


namespace Stock_Back.BLL.Models.FinancialMovementsModelDTO
{
    /// <summary>
    /// Data Transfer Object for inserting a new FinancialMovements.
    /// </summary>
    public class FinancialMovementsInsertDTO
    {
        /// <summary>
        /// Name of the FinancialMovements. Required.
        /// </summary>
        [Required]
        public string? Name { get; set; }
        /// <summary>
        /// Description of the FinancialMovements. Required.
        /// </summary>
        [Required]
        public string? Description { get; set; }

        [Required]
        public int? FinancialSubCategoryId { get; set; }
        
        [Required]
        public int? ReferenceId { get; set; }
        [Required]
        public string? Bank { get; set; }
        public decimal Amout { get; set; }
        [Required]
        public DateTime DocumentDate { get; set; }
    }
}
