﻿using System.ComponentModel.DataAnnotations;
using Stock_Back.BLL.Models.FinancialMovementsModelDTO;

namespace Stock_Back.BLL.Models.FinancialSubCategoryModelDTO
{
    /// <summary>
    /// Data Transfer Object (DTO) representing a FinancialSubCategory with full details.
    /// </summary>
    public class FinancialSubCategoryFullDTO : FinancialSubCategoryBaseGDTO
    {
        public virtual IEnumerable<FinancialMovementsDTO> FinancialMovements { get; set; } = Enumerable.Empty<FinancialMovementsDTO>();
    }
}
