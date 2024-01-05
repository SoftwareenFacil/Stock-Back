using Microsoft.EntityFrameworkCore;
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

        public async Task<User?> UpdateUser(User user)
        {
            var response = await _context.Users.Where(userAux => userAux.Id.Equals(user.Id)).FirstOrDefaultAsync();
            if (response != null)
            {
                response.Name = user.Name;
                response.Email = user.Email;
                response.Password = user.Password;
                response.Phone = user.Phone;

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
