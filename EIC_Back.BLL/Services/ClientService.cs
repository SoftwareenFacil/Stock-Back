using AutoMapper;
using EIC_Back.BLL.EnumExtensions;
using EIC_Back.BLL.Models.ClientModelDTO;
using EIC_Back.DAL.Context;
using EIC_Back.DAL.Models;
using EIC_Back.DAL.Repository;

namespace EIC_Back.BLL.Services
{
    public class ClientService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ClientRepository _clientRepository;

        public ClientService(AppDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
            _clientRepository = new ClientRepository(dbContext);
        }
        public async Task<bool> DeleteClientById(int id)
        {
            var exist = await _clientRepository.GetClientById(id);
            if (exist == null)
            {
                return false;
            }

            return await _clientRepository.DeleteClient(id);
        }
        public async Task<(bool, bool)> UpdateClient(ClientEditDTO clientEdited)
        {
            bool isUpdated = false;
            bool isClient = false;
            if (string.IsNullOrWhiteSpace(clientEdited.Name) && string.IsNullOrWhiteSpace(clientEdited.Email) && clientEdited.Phone == null && string.IsNullOrWhiteSpace(clientEdited.Address) && string.IsNullOrWhiteSpace(clientEdited.TaxId))
                return (isUpdated, isClient);


            var client = await _clientRepository.GetClientById(clientEdited.Id);
            if (client != null)
            {
                isClient = true;

                if (!await ValidateClientEmailAndTaxId(clientEdited, client))
                {

                    client.Name = !string.IsNullOrEmpty(clientEdited.Name) ? clientEdited.Name : client.Name;
                    client.Email = !string.IsNullOrEmpty(clientEdited.Email) ? clientEdited.Email : client.Email;
                    client.Phone = clientEdited.Phone != null ? clientEdited.Phone.ToString() : client.Phone;
                    client.Address = !string.IsNullOrEmpty(clientEdited.Address) ? clientEdited.Address : client.Address;
                    client.TaxId = !string.IsNullOrEmpty(clientEdited.TaxId) ? clientEdited.TaxId : client.TaxId;
                    isUpdated = await _clientRepository.UpdateClient(client);
                }
                return (isUpdated, isClient);
            }
            return (isUpdated, isClient);
        }
        public async Task<int> AddClient(ClientInsertDTO client)
        {

            if (await _clientRepository.GetClientByEmail(client.Email) != null)
                return -1;
            if (await _clientRepository.GetClientByTaxId(client.TaxId) != null)
                return -1;

            DateTime utcNow = DateTime.UtcNow;
            var clientCreate = new Client()
            {
                Name = client.Name,
                Email = client.Email,
                Phone = client.Phone,
                TaxId = client.TaxId,
                Address = client.Address,
                Created = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc),
                Updated = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc),
                Vigency = true
            };

            if (await _clientRepository.InsertClient(clientCreate) > 0)
                return clientCreate.Id;
            return 0;

        }

        public async Task<dynamic?> GetClients(string value, Identifier identifier)
        {
            return identifier switch
            {
                Identifier.Unknown or Identifier.All => await GetAllClients(),
                Identifier.Name => await GetByName(value),
                Identifier.TaxId => await GetByTaxId(value),
                Identifier.Id => await GetClientById(value),
                Identifier.CreatedDate => await GetByCreatedDate(value),
                Identifier.Vigency => await GetByVigency(value),
                Identifier.Email => await GetByEmail(value),
                Identifier.Phone => await GetByPhone(value),
                _ => null
            };
        }

        public async Task<ClientDTO?> GetByPhone(string value)
        {
            // var success = int.TryParse(value, out int phone);
            // if (!success) return null;

            if (string.IsNullOrWhiteSpace(value) && string.IsNullOrEmpty(value))
                return null;

            var client = await _clientRepository.GetClientByPhone(value);

            return _mapper.Map<ClientDTO?>(client);
        }

        public async Task<ClientDTO?> GetByEmail(string value)
        {
            var _clientRepository = new ClientRepository(_context);
            var client = await _clientRepository.GetClientByEmail(value);

            return _mapper.Map<ClientDTO?>(client);
        }

        public async Task<IEnumerable<ClientDTO>?> GetByVigency(string value)
        {
            var success = bool.TryParse(value, out bool vigency);
            if (!success) return null;

            var clients = await _clientRepository.GetClientsByVigency(vigency);

            return _mapper.Map<IEnumerable<ClientDTO>?>(clients);
        }

        public async Task<IEnumerable<ClientDTO>?> GetByCreatedDate(string value)
        {
            var success = DateTime.TryParse(value, out DateTime date);
            if (!success) return null;

            var clients = await _clientRepository.GetClientsByCreatedDate(date);

            return _mapper.Map<IEnumerable<ClientDTO>?>(clients);
        }

        public async Task<ClientDTO?> GetByTaxId(string value)
        {
            var client = await _clientRepository.GetClientByTaxId(value);

            return _mapper.Map<ClientDTO?>(client);
        }

        public async Task<IEnumerable<ClientDTO>?> GetByName(string value)
        {
            var clients = await _clientRepository.GetClientsByName(value);

            return _mapper.Map<IEnumerable<ClientDTO>?>(clients);
        }

        public async Task<FullClientDTO?> GetClientById(string value)
        {
            var success = int.TryParse(value, out int id);
            if (!success) return null;

            var client = await _clientRepository.GetClientById(id);

            return _mapper.Map<FullClientDTO?>(client);
        }

        public async Task<IEnumerable<ClientDTO>?> GetAllClients()
        {
            var clients = await _clientRepository.GetAllClients();
            var clientDTOs = clients.Select(data => new ClientDTO
            {
                Id = data.Id,
                Name = data.Name,
                Email = data.Email,
                Phone = data.Phone,
                Address = data.Address,
                TaxId = data.TaxId,
                Created = data.Created,
                Vigency = data.Vigency
            }).ToList();
            return clientDTOs;
        }

        private async Task<bool> ValidateClientEmailAndTaxId(ClientEditDTO edit, Client client)
        {
            if(!String.IsNullOrEmpty(edit.Email) && edit.Email != client.Email)
                if (await _clientRepository.GetClientByEmail(edit.Email) != null)
                    return true;
            if(!String.IsNullOrEmpty(edit.TaxId) && edit.TaxId != client.TaxId)
                if (await _clientRepository.GetClientByTaxId(client.TaxId) != null)
                    return true;
            return false;
        }
    }
}
