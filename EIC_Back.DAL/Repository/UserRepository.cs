using Microsoft.EntityFrameworkCore;
using EIC_Back.DAL.Context;
using EIC_Back.DAL.Models;

namespace EIC_Back.DAL.Repository
{
    public class UserRepository
    {
        private AppDbContext _context;
        public UserRepository(AppDbContext _dbContext)
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
                DateTime utcNow = DateTime.UtcNow;
                TimeZoneInfo chileTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific SA Standard Time");
                DateTime chileTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, chileTimeZone);
                response.Updated = DateTime.SpecifyKind(chileTime, DateTimeKind.Utc);

                if (await _context.SaveChangesAsync() > 0)
                    return true;

            }
            return false;
        }
        public async Task<List<User>> GetAllUsers()
        {
            var users = await _context.Users.Take(100).ToListAsync();
            return users;
        }
        public async Task<User> GetUserById(int id)
        {
            var response = await _context.Users.Where(userAux => userAux.Id.Equals(id)).FirstOrDefaultAsync();
            return response;
        }

        public async Task<int> InsertUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            return user;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users.Where(userAux => userAux.Id.Equals(id)).FirstOrDefaultAsync();
            _context.Users.Remove(user);
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<User> GetUserByRefreshToken(string token, DateTime date)
        {
            var user = await _context.Users.Where(u => u.RefreshTokenDate <= date && u.RefreshToken == token).FirstOrDefaultAsync();
            return user;
        }

        public async Task<bool> SaveRefresh(string token, User user)
        {
            user.RefreshToken = token;
            user.RefreshTokenDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
            return await _context.SaveChangesAsync() > 0;
        }


    }
}
