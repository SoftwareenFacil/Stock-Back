using Microsoft.EntityFrameworkCore;
using Stock_Back.DAL.Context;
using Stock_Back.DAL.Models;

namespace Stock_Back.DAL.Controllers.ClientControllers
{
    public class ClientGetAll
    {
        private AppDbContext _context;
        public ClientGetAll(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<List<Client>> GetAllClients()
        {
            List<Client> clients = new List<Client>();
            var dataList = await _context.Clients.Take(100).ToListAsync();
            dataList.ForEach(row => clients.Add(new Client()
            {
                Id = row.Id,
                Name = row.Name,
                Email = row.Email,
                Phone = row.Phone,
                TaxID = row.TaxID,
                Created = row.Created,
                Updated = row.Updated
            }));
            return clients;
        }
    }
}
