﻿using Microsoft.EntityFrameworkCore;
using Stock_Back.DAL.Data;
using Stock_Back.DAL.Models;

namespace Stock_Back.DAL.Controller.UserControllers
{
    public class UserGetById
    {
        private AppDbContext _context;
        public UserGetById(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<User?> GetUserById(int id)
        {
            User? response = new User();
            response = await _context.Users.Where(userAux => userAux.Id.Equals(id)).FirstOrDefaultAsync();
            if (response != null)
            {
                return new User()
                {
                    Id = response.Id,
                    Name = response.Name,
                    Email = response.Email,
                    Password = response.Password,
                    Created = DateTime.Now.ToUniversalTime(),
                    Updated = DateTime.Now.ToUniversalTime()
                };
            }

            return response;
        }
    }

}