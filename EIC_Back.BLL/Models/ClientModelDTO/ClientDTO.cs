using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIC_Back.BLL.Models.ClientModelDTO
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string TaxId { get; set; }
        public string Address { get; set; } = string.Empty;

        public bool Vigency { get; set; } = true;

        public int ProjectCount { get; set; } = 0;
        public DateTime Created { get; set; }
    }
}
