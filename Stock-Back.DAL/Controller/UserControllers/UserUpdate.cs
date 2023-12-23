using Microsoft.EntityFrameworkCore;
using Stock_Back.DAL.Data;
using Stock_Back.DAL.Models;

namespace Stock_Back.DAL.Controller.UserControllers
{
    public class UserUpdate
    {
        private AppDbContext _context;
        public UserUpdate(AppDbContext _dbContext)
        {
            _context = _dbContext;
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
    }
}
