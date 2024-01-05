using Microsoft.EntityFrameworkCore;
using Stock_Back.DAL.Context;
using Stock_Back.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Back.DAL.Controllers.UserControllers
{
    public class UserGetByEmail
    {
        private AppDbContext _context;
        public UserGetByEmail(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            return user;
        }
    }
}
