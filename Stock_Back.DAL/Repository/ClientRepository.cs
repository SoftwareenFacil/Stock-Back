using Microsoft.EntityFrameworkCore;
using Stock_Back.DAL.Context;
using Stock_Back.DAL.Models;

namespace Stock_Back.DAL.Repository
{
    public class ClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Client?> GetClientByEmail(string? email)
        {
            return await _context.Clients.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<bool> DeleteClient(int id)
        {
            var client = await _context.Clients.Where(clientAux => clientAux.Id.Equals(id)).FirstOrDefaultAsync();
            _context.Clients.Remove(client);
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<Client> GetClientById(int id)
        {
            var response = await _context.Clients.Where(clientAux => clientAux.Id.Equals(id)).FirstOrDefaultAsync();
            return response;
        }
        public async Task<Client?> GetClientByPhone(string phone)
        {
            return await _context.Clients.FirstOrDefaultAsync(x => x.Phone == phone);
        }
        public async Task<Client?> GetClientByTaxId(string? taxId)
        {
            return await _context.Clients.FirstOrDefaultAsync(x => x.TaxId == taxId);
        }
        public async Task<int> InsertClient(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
            return client.Id;
        }
        public async Task<List<Client>> GetAllClients()
        {
            var clients = await _context.Clients.Take(100).ToListAsync();
            return clients;
        }
        public async Task<IEnumerable<Client>> GetClientsByCreatedDate(DateTime date)
        {
            var utcDate = date.Date.ToUniversalTime();
            return await _context.Clients
                .Where(x => x.Created.Date == utcDate.Date)
                .Take(100)
                .ToListAsync();
        }
        public async Task<IEnumerable<Client>> GetClientsByName(string? name)
        {
            return await _context.Clients
                .Where(x => x.Name == name)
                .Take(100)
                .ToListAsync();
        }
        public async Task<IEnumerable<Client>> GetClientsByVigency(bool vigency)
        {
            return await _context.Clients
                .Where(x => x.Vigency == vigency)
                .Take(100)
                .ToListAsync();
        }
        public async Task<bool> UpdateClient(Client client)
        {
            var response = await _context.Clients.Where(clientAux => clientAux.Id.Equals(client.Id)).FirstOrDefaultAsync();
            if (response != null)
            {
                response.Name = client.Name;
                response.Email = client.Email;
                response.Phone = client.Phone;
                response.TaxId = client.TaxId;
                DateTime utcNow = DateTime.UtcNow;
                TimeZoneInfo chileTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific SA Standard Time");
                DateTime chileTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, chileTimeZone);
                response.Updated = DateTime.SpecifyKind(chileTime, DateTimeKind.Utc);

                if (await _context.SaveChangesAsync() > 0)
                    return true;

            }
            return false;
        }
    }
}
