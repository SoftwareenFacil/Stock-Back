﻿using System.ComponentModel.DataAnnotations;
using EIC_Back.BLL.Models.FinancialSubCategoryModelDTO;
using EIC_Back.DAL.Models;

namespace EIC_Back.BLL.Models.FinancialCategoryModelDTO
{
    /// <summary>
    /// Data Transfer Object (DTO) representing a Category with full details.
    /// </summary>
    public class FinancialCategoryFullDTO : FinancialCategoryBaseGDTO
    {
        public virtual IEnumerable<FinancialSubCategoryFullDTO> FinancialSubCategory { get; set; } = Enumerable.Empty<FinancialSubCategoryFullDTO>();
    }
}