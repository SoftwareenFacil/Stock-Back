using Microsoft.EntityFrameworkCore;
using Stock_Back.DAL.Context;
using Stock_Back.DAL.Models;

namespace Stock_Back.DAL.Controllers.UserControllers
{
    public class UserGetAll
    {
        private AppDbContext _context;
        public UserGetAll(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _context.Users.Take(100).ToListAsync();
            return users;
        }

    }
}
