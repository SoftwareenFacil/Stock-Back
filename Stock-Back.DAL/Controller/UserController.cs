using Microsoft.EntityFrameworkCore;
using Stock_Back.DAL.Data;
using Stock_Back.DAL.Interfaces;
using Stock_Back.DAL.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public async Task<User?> InsertUser(User user)
        {
            User? response = new User();
            if (!string.IsNullOrWhiteSpace(user.Name) &&
                !string.IsNullOrWhiteSpace(user.Email) &&
                !string.IsNullOrWhiteSpace(user.Password))
            {
                response.Name = user.Name;
                response.Email = user.Email;
                response.Password = user.Password;

                // Generar la hora actual en UTC
                DateTime utcNow = DateTime.UtcNow; // Es más directo y recomendable usar DateTime.UtcNow
                TimeZoneInfo chileTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific SA Standard Time"); // Para sistemas Windows
                DateTime chileTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, chileTimeZone);

                response.Created = DateTime.SpecifyKind(chileTime, DateTimeKind.Utc);
                response.Updated = DateTime.SpecifyKind(chileTime, DateTimeKind.Utc);

                await _context.Users.AddAsync(response);
                await _context.SaveChangesAsync();
            }
            else
            {
                return null;
            }
            
            return response;
            
        }

        public async Task<User?> UpdateUser(User user)
        {
            var response = await _context.Users.Where(userAux => userAux.Id.Equals(user.Id)).FirstOrDefaultAsync();
            if (response != null)
            {
                if (!string.IsNullOrWhiteSpace(user.Name))
                {
                    response.Name = user.Name;
                }

                if (!string.IsNullOrWhiteSpace(user.Email))
                {
                    response.Email = user.Email;
                }

                if (!string.IsNullOrWhiteSpace(user.Password))
                {
                    response.Password = user.Password;
                }

                // Generar la hora actual en UTC
                DateTime utcNow = DateTime.UtcNow; // Es más directo y recomendable usar DateTime.UtcNow
                TimeZoneInfo chileTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific SA Standard Time"); // Para sistemas Windows
                DateTime chileTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, chileTimeZone);
                response.Updated = DateTime.SpecifyKind(chileTime, DateTimeKind.Utc);

                await _context.SaveChangesAsync();

            }
            return response;
        }

        public async Task<int> GetUserIdByEmail(string email)
        {
            var user = await _context.Users
                             .Where(u => u.Email == email)
                             .FirstOrDefaultAsync();
            return user?.Id ?? 0;
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

        public async Task<bool> UserEmailExist(string email)
        {
            var user = await _context.Users
                             .FirstOrDefaultAsync(u => u.Email == email);

            // Retorna true si se encontró un usuario, false en caso contrario.
            return user != null;
        }
    }
}
