using Microsoft.EntityFrameworkCore;
using Stock_Back.DAL.Context;
using Stock_Back.DAL.Models;

namespace Stock_Back.DAL.Controllers.ClientControllers
{
    public class ClientGetByEmail
    {
        private AppDbContext _context;
        public ClientGetByEmail(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<Client> GetClientByEmail(string email)
        {
            var client = await _context.Clients.Where(u => u.Email == email).FirstOrDefaultAsync();
            return client;
        }
    }
}
