using Microsoft.EntityFrameworkCore;
using Stock_Back.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Back.DAL.Controller.UserControllers
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

            // Retorna true si se encontró un usuario, false en caso contrario.
            return user != null;
        }
    }
}
