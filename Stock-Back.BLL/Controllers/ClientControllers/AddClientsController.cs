using Stock_Back.BLL.Models.ClientDTO;
using Stock_Back.DAL.Context;
using Stock_Back.DAL.Controllers.ClientControllers;
using Stock_Back.DAL.Models;

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

            var clientCreator = new ClientPost(_context);
            var clientCreate = new Client();

            clientCreate.Name = client.Name;
            clientCreate.Email = client.Email;
            clientCreate.Phone = client.Phone;
            clientCreate.TaxID = "0";

            DateTime utcNow = DateTime.UtcNow;
            TimeZoneInfo chileTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific SA Standard Time");
            DateTime chileTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, chileTimeZone);

            clientCreate.Created = DateTime.SpecifyKind(chileTime, DateTimeKind.Utc);
            clientCreate.Updated = DateTime.SpecifyKind(chileTime, DateTimeKind.Utc);
            return await clientCreator.InsertClient(clientCreate);

        }
    }
}
