using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Back.BLL.Models.ClientDTO
{
    public class ClientInsertDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string TaxId { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; } = string.Empty;
    }
}
