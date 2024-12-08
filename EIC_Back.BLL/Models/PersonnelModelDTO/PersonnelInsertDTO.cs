using System.ComponentModel.DataAnnotations;

namespace EIC_Back.BLL.Models.PersonnelModelDTO
{
    public class PersonnelInsertDTO
    {
        [Required]
        public string? Name { get; set; }
        public string? Specialty { get; set; }
        public int PricePerWorkDay { get; set; }
        [Required]
        public int TaxId { get; set; }

        public string? Phone { get; set; }
        [Required]
        public string? Email { get; set; }
    }
}
