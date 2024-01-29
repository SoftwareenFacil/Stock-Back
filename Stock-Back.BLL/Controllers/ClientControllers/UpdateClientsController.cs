using Stock_Back.BLL.Models.ClientDTO;
using Stock_Back.DAL.Context;
using Stock_Back.DAL.Controllers.ClientControllers;

namespace Stock_Back.BLL.Controllers.ClientControllers
{
    public class UpdateClientsController
    {
        private AppDbContext _context;
        public UpdateClientsController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<int> UpdateClient(ClientEditDTO clientEdited)
        {
            if (string.IsNullOrWhiteSpace(clientEdited.Name) && string.IsNullOrWhiteSpace(clientEdited.Email) && clientEdited.Phone == 0 && string.IsNullOrWhiteSpace(clientEdited.Address) && string.IsNullOrWhiteSpace(clientEdited.TaxId))
                return 500;

            var clientVerify = new ClientGetById(_context);
            var clientUpdater = new ClientUpdate(_context);
            var client = await clientVerify.GetClientById(clientEdited.Id);
            if (client != null)
            {
                client.Name = !string.IsNullOrEmpty(clientEdited.Name) ? clientEdited.Name : client.Name;
                client.Email = !string.IsNullOrEmpty(clientEdited.Email) ? clientEdited.Email : client.Email;
                client.Phone = clientEdited.Phone > 0 ? clientEdited.Phone : client.Phone;
                client.Address = !string.IsNullOrEmpty(clientEdited.Address) ? clientEdited.Address : client.Address;
                client.TaxId = !string.IsNullOrEmpty(clientEdited.TaxId) ? clientEdited.TaxId : client.TaxId;

                DateTime utcNow = DateTime.UtcNow;
                client.Updated = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc);

                var isUpdated = await clientUpdater.UpdateClient(client);
                if (!isUpdated)
                    return 500;
                return 200;
            }
            return 404;
        }
    }
}
