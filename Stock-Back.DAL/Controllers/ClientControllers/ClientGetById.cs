using Microsoft.EntityFrameworkCore;
using Stock_Back.DAL.Context;
using Stock_Back.DAL.Models;

namespace Stock_Back.DAL.Controllers.ClientControllers
{
    public class ClientGetById
    {
        private AppDbContext _context;
        public ClientGetById(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<Client> GetClientById(int id)
        {
            var response = await _context.Clients.Where(clientAux => clientAux.Id.Equals(id)).FirstOrDefaultAsync();
            return response;
        }
    }
}
