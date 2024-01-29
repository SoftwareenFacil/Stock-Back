using Stock_Back.BLL.Models.ClientDTO;
using Stock_Back.DAL.Context;
using Stock_Back.DAL.Controllers.ClientControllers;

namespace Stock_Back.BLL.Controllers.ClientControllers
{
    public class GetClientsController
    {
        private AppDbContext _context;
        public GetClientsController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<List<ClientDTO>> GetClientBy(int? id, string? name, string? email, string? taxId, DateTime? created, bool? vigency)
        {
            var clientGetter = new ClientGetBy(_context);
            var clients = await clientGetter.GetClientBy(id, name, email, taxId, created, vigency);
            if (clients.Count() > 0)
            {
                List<ClientDTO> result = new List<ClientDTO>();
                clients.ForEach(row => result.Add(new ClientDTO()
                {
                    Id = row.Id,
                    Name = row.Name,
                    Email = row.Email,
                    TaxId = row.TaxId,
                    Phone = row.Phone,
                    Address = row.Address
                }));
                return result;
            }

            return new List<ClientDTO>();
        }

    }
}
