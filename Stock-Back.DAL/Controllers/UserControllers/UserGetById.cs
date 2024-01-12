using Microsoft.EntityFrameworkCore;
using Stock_Back.DAL.Context;
using Stock_Back.DAL.Models;

namespace Stock_Back.DAL.Controllers.UserControllers
{
    public class UserGetById
    {
        private AppDbContext _context;
        public UserGetById(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<User> GetUserById(int id)
        {
            var response = await _context.Users.Where(userAux => userAux.Id.Equals(id)).FirstOrDefaultAsync();
            return response;
        }
    }

}
