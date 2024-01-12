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

        public async Task<dynamic?> GetClients(int id)
        {
            if (id == 0)
                return await GetAllClients();
            return await GetClientById(id);
        }

        public async Task<ClientDTO?> GetClientById(int id)
        {
            var clientGetter = new ClientGetById(_context);
            var client = await clientGetter.GetClientById(id);
            if (client == null)
            {
                return null;
            }
            else
            {
                return new ClientDTO()
                {
                    Id = client.Id,
                    Name = client.Name,
                    Email = client.Email,
                    Phone = client.Phone,
                    Address = client.Address
                };
            }
        }

        public async Task<List<ClientDTO>?> GetAllClients()
        {
            var clientGetter = new ClientGetAll(_context);
            var clients = await clientGetter.GetAllClients();

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
            else
            {
                return null;
            }

        }
    }
}
