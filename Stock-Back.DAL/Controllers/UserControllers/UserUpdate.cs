﻿using Microsoft.EntityFrameworkCore;
using Stock_Back.DAL.Context;
using Stock_Back.DAL.Models;

namespace Stock_Back.DAL.Controllers.UserControllers
{
    public class UserUpdate
    {
        private AppDbContext _context;
        public UserUpdate(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<bool> UpdateUser(User user)
        {
            var response = await _context.Users.Where(userAux => userAux.Id.Equals(user.Id)).FirstOrDefaultAsync();
            if (response != null)
            {
                response.Name = user.Name;
                response.Email = user.Email;
                response.Password = user.Password;
                response.Phone = user.Phone;
                response.Address = user.Address;
                response.Updated = user.Updated;

                if (await _context.SaveChangesAsync() > 0)
                    return true;

            }
            return false;
        }
    }
}
