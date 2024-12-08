using System.ComponentModel.DataAnnotations;

namespace EIC_Back.BLL.Models.PersonnelModelDTO
{
    public class PersonnelEditDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Specialty { get; set; }
        public int PricePerWorkDay { get; set; }
        [Required]
        public int TaxId { get; set; }
        public string? Phone { get; set; }
        [Required]
        public string? Email { get; set; }

        public int? projectId { get; set; } = null;
    }
}
