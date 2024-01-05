using Microsoft.EntityFrameworkCore;
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
    }

}
