using Stock_Back.BLL.Models.ClientDTO;
using Stock_Back.DAL.Context;
using Stock_Back.DAL.Controllers.ClientControllers;
using Stock_Back.DAL.Models;
using System.Numerics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Stock_Back.BLL.Controllers.ClientControllers
{
    public class AddClientsController
    {
        private AppDbContext _context;
        public AddClientsController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<int> AddClient(ClientInsertDTO client)
        {

            var clientGetter = new ClientGetByEmail(_context);
            if (await clientGetter.GetClientByEmail(client.Email) != null)
                return -1;

            DateTime utcNow = DateTime.UtcNow;
            TimeZoneInfo chileTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific SA Standard Time");
            DateTime chileTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, chileTimeZone);
            var clientCreator = new ClientPost(_context);
            var clientCreate = new Client()
            {
                Name = client.Name,
                Email = client.Email,
                Phone = client.Phone,
                TaxId = client.TaxId,
                Address = client.Address,
                Created = DateTime.SpecifyKind(chileTime, DateTimeKind.Utc),
                Updated = DateTime.SpecifyKind(chileTime, DateTimeKind.Utc)
            };
            
            return await clientCreator.InsertClient(clientCreate);

        }
    }
}
