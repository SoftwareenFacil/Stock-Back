using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIC_Back.BLL.Models.ClientModelDTO
{
    public class ClientInsertDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string TaxId { get; set; }
        public string? Phone { get; set; }
        public string Address { get; set; } = string.Empty;
    }
}
