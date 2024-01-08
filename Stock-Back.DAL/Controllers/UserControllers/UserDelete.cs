using Microsoft.EntityFrameworkCore;
using Stock_Back.DAL.Context;
using Stock_Back.DAL.Models;

namespace Stock_Back.DAL.Controllers.UserControllers
{
    public class UserDelete
    {
        private AppDbContext _context;
        public UserDelete(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<bool> DeleteUser(int id)
        {
            User? user = new User();
            user = await _context.Users.Where(userAux => userAux.Id.Equals(id)).FirstOrDefaultAsync();
            if (user != null)
            {
                //TODO: check on all methods on database
                _context.Users.Remove(user);
                if (await _context.SaveChangesAsync() > 0)
                    return true;

            }
            return false;
        }
    }
}
