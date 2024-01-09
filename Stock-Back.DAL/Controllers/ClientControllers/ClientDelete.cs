using Microsoft.EntityFrameworkCore;
using Stock_Back.DAL.Context;
using Stock_Back.DAL.Models;

namespace Stock_Back.DAL.Controllers.ClientControllers
{
    public class ClientDelete
    {
        private AppDbContext _context;
        public ClientDelete(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<bool> DeleteClient(int id)
        {
            Client? client = new Client();
            client = await _context.Clients.Where(clientAux => clientAux.Id.Equals(id)).FirstOrDefaultAsync();
            if (client != null)
            {
                //TODO: check on all methods on database //  nvargas: BLL is cheking the object life, its redundant
                _context.Clients.Remove(client);
                if (await _context.SaveChangesAsync() > 0)
                    return true;

            }
            return false;
        }
    }
}
