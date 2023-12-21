using Microsoft.EntityFrameworkCore;
using Stock_Back.DAL.Data;
using Stock_Back.DAL.Interfaces;
using Stock_Back.DAL.Models;

namespace Stock_Back.DAL.Controller
{
    public class UserController : IUserController
    {
        private AppDbContext _context;
        public UserController(AppDbContext context)
        {
            _context = context;
        }

        //GET
        public async Task<List<User>> GetAllUsers()
        {
            List<User> users = new List<User>();
            var dataList = await _context.Users.ToListAsync();
            dataList.ForEach(row => users.Add(new User()
            {
                Id = row.Id,
                Name = row.Name,
                Email = row.Email,
                Password = row.Password,
                Created = row.Created,
                Updated = row.Updated
            }));
            return users;
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

        //POST/PUT/PATCH
        public async Task<bool> InsertUser(User user)
        {
            if (user.Id > 0) 
            {
                User response = new User();
                response.Name = user.Name;
                response.Email = user.Email;
                response.Password = user.Password; 
                response.Created = DateTime.Now.ToUniversalTime();
                response.Updated= DateTime.Now.ToUniversalTime();
                await _context.Users.AddAsync(response);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<User?> UpdateUser(User user)
        {
            User? response = new User();
            if (user.Id > 0)
            {
                
                response = await _context.Users.Where(userAux => userAux.Id.Equals(user.Id)).FirstOrDefaultAsync();
                if (response != null)
                {
                    response.Name = user.Name;
                    response.Email = user.Email;
                    response.Password = user.Password;
                    response.Updated = DateTime.Now.ToUniversalTime();
                    await _context.SaveChangesAsync();
                    
                }
                
            }
            return response;
        }

        public async Task<bool> DeleteUser(int id)
        {
            User? user = new User();
            user = await _context.Users.Where(userAux => userAux.Id.Equals(id)).FirstOrDefaultAsync();
            if(user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
