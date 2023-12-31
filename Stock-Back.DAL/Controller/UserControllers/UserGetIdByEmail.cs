using Microsoft.EntityFrameworkCore;
using Stock_Back.DAL.Data;

namespace Stock_Back.DAL.Controller.UserControllers
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
