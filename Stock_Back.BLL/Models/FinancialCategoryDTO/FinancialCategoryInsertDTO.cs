using System.ComponentModel.DataAnnotations;


namespace Stock_Back.BLL.Models.FinancialCategoryModelDTO
{
    /// <summary>
    /// Data Transfer Object for inserting a new Category.
    /// </summary>
    public class FinancialCategoryInsertDTO
    {
        /// <summary>
        /// Name of the category. Required.
        /// </summary>
        [Required]
        public string? Name { get; set; }

        /// <summary>
        /// Description of the category. Required.
        /// </summary>
        [Required]
        public string? Description { get; set; }
        /// <summary>
        /// Movement type  I = Income / E = Expense
        /// </summary>
        public string? Type { get; set; } //Ingreso o Egreso
    }
}
