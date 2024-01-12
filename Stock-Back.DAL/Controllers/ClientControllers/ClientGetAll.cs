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
            var clients = await _context.Clients.Take(100).ToListAsync();
            return clients;
        }
    }
}
