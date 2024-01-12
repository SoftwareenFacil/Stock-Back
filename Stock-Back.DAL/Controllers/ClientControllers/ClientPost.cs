using Stock_Back.DAL.Context;
using Stock_Back.DAL.Models;

namespace Stock_Back.DAL.Controllers.ClientControllers
{
    public class ClientPost
    {
        private AppDbContext _context;
        public ClientPost(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<int> InsertClient(Client client)
        {
            await _context.Clients.AddAsync(client);
            return await _context.SaveChangesAsync();
        }
    }
}
