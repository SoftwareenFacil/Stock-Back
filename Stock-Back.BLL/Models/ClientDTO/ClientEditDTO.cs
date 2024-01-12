using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Back.BLL.Models.ClientDTO
{
    public class ClientEditDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string TaxId { get; set; } = string.Empty;
        public int Phone { get; set; } = 0;
        public string Address { get; set; } = string.Empty;
    }
}
