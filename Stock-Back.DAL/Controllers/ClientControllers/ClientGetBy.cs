using Microsoft.EntityFrameworkCore;
using Stock_Back.DAL.Context;
using Stock_Back.DAL.Models;

namespace Stock_Back.DAL.Controllers.ClientControllers
{
    public class ClientGetBy
    {
        private AppDbContext _context;
        public ClientGetBy(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<List<Client>> GetClientBy(int? id, string? name, string? email, string? taxId, DateTime? created, bool? vigency)
        {
            if (id.HasValue)
            {
                if (id.Value == 0)
                {
                    return await _context.Clients.Take(100).ToListAsync();
                }
                else
                {
                    var client = await _context.Clients.FirstOrDefaultAsync(u => u.Id == id.Value);
                    return client == null ? new List<Client>() : new List<Client> { client };
                }
            }

            var query = _context.Clients.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(u => EF.Functions.Like(u.Name, $"%{name}%"));
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                query = query.Where(u => EF.Functions.Like(u.Email, $"%{email}%"));
            }

            if (!string.IsNullOrWhiteSpace(taxId))
            {
                query = query.Where(u => EF.Functions.Like(u.TaxId, $"%{taxId}%"));
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
