using Stock_Back.DAL.Context;
using Stock_Back.DAL.Controllers.ClientControllers;

namespace Stock_Back.BLL.Controllers.ClientControllers
{
    public class DeleteClientsController
    {
        private AppDbContext _context;
        public DeleteClientsController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<bool> DeleteClientById(int id)
        {
            var clientVerify = new ClientGetById(_context);
            var exist = await clientVerify.GetClientById(id);
            if (exist == null)
            {
                return false;
            }

            var clientDeleter = new ClientDelete(_context);
            return await clientDeleter.DeleteClient(id);
        }
    }
}
