using Stock_Back.DAL.Context;
using Stock_Back.DAL.Models;

namespace Stock_Back.DAL.Controllers.UserControllers
{
    public class UserPost
    {
        private AppDbContext _context;
        public UserPost(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<int> InsertUser(User user)
        {
            await _context.Users.AddAsync(user);
            return await _context.SaveChangesAsync();
        }
    }

}
