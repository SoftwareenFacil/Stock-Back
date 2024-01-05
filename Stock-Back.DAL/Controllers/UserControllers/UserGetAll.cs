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
            List<User> users = new List<User>();
            var dataList = await _context.Users.Take(100).ToListAsync();
            dataList.ForEach(row => users.Add(new User()
            {
                Id = row.Id,
                Name = row.Name,
                Email = row.Email,
                Password = row.Password,
                Created = row.Created,
                Updated = row.Updated,
                SuperAdmin = row.SuperAdmin
            }));
            return users;
        }

    }
}
