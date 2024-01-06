using Microsoft.EntityFrameworkCore;
using Stock_Back.DAL.Context;

namespace Stock_Back.DAL.Controllers.UserControllers
{
    public class UserGetIdByEmail
    {
        private AppDbContext _context;
        public UserGetIdByEmail(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<int> GetUserIdByEmail(string email)
        {
            var user = await _context.Users
                             .Where(u => u.Email == email)
                             .FirstOrDefaultAsync();
            return user?.Id ?? 0;
        }
    }
}
