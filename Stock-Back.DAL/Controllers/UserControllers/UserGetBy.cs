using Microsoft.EntityFrameworkCore;
using Stock_Back.DAL.Context;
using Stock_Back.DAL.Models;

namespace Stock_Back.DAL.Controllers.UserControllers
{
    public class UserGetBy
    {
        private AppDbContext _context;
        public UserGetBy(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<User>> GetUserBy(int? id, string? name, string? email, DateTime? created, bool? vigency)
        {
            if (id.HasValue)
            {
                if (id.Value == 0)
                {
                    return await _context.Users.Take(100).ToListAsync();
                }
                else
                {
                    var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id.Value);
                    return user == null ? new List<User>() : new List<User> { user };
                }
            }

            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(u => EF.Functions.Like(u.Name, $"%{name}%"));
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                query = query.Where(u => EF.Functions.Like(u.Email, $"%{email}%"));
            }

            if (created.HasValue)
            {
                query = query.Where(u => u.Created > created.Value)
                             .OrderBy(u => u.Created);
            }

            if (vigency.HasValue)
            {
                query = query.Where(u => u.Vigency == vigency.Value);
            }

            return await query.Take(100).ToListAsync();
        }
    }
}
