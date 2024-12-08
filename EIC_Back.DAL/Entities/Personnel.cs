using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EIC_Back.DAL.Models
{
    [Table("Personnel")]
    public class Personnel
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Specialty { get; set; }
        public int PricePerWorkDay { get; set; }

        public int TaxId { get; set; }
        public string? Phone { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

    }
}
