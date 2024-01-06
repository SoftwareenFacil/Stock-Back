using Microsoft.EntityFrameworkCore;
using Stock_Back.DAL.Context;

namespace Stock_Back.DAL.Controllers.UserControllers
{
    public class UserExistsById
    {
        private AppDbContext _context;
        public UserExistsById(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<bool> UserEmailExists(string email)
        {
            var user = await _context.Users
                             .FirstOrDefaultAsync(u => u.Email == email);

            return user != null;
        }
    }
}
