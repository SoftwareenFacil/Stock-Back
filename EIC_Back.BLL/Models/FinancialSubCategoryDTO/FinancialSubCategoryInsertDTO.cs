using System.ComponentModel.DataAnnotations;


namespace EIC_Back.BLL.Models.FinancialSubCategoryModelDTO
{
    /// <summary>
    /// Data Transfer Object for inserting a new FinancialSubCategory.
    /// </summary>
    public class FinancialSubCategoryInsertDTO
    {
        /// <summary>
        /// Name of the FinancialSubCategory. Required.
        /// </summary>
        [Required]
        public string? Name { get; set; }
        /// <summary>
        /// Description of the FinancialSubCategory. Required.
        /// </summary>
        [Required]
        public string? Description { get; set; }

        [Required]
        public int? FinancialCategoryId { get; set; }
    }
}
