using System.ComponentModel.DataAnnotations;

namespace EIC_Back.BLL.Models.PersonnelModelDTO
{
    public class PersonnelDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Specialty { get; set; }
        public int PricePerWorkDay { get; set; }

        public int LastProyectDesignated { get; set; }

        public string LastProyectName { get; set; }
        public int TaxId { get; set; }
        public string? Phone { get; set; }
        [Required]
        public string? Email { get; set; }
        public DateTime Created { get; set; }
    }
}
