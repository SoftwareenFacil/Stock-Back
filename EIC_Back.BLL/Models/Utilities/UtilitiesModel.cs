using Microsoft.AspNetCore.Builder;

namespace EIC_Back.BLL.Models.Utilities;

public class UtilitiesModel
{
    public decimal TotalIncome { get; set; }
    public decimal TotalExpense { get; set; }
    public decimal TotalUtilities { get => TotalIncome / TotalExpense * 100; }
}