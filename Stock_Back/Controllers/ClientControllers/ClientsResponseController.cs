using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock_Back.BLL.EnumExtensions;
using Stock_Back.Controllers.Services;
using Stock_Back.DAL.Context;
using Stock_Back.Models;
using Stock_Back.BLL.Models.ClientModelDTO;
using Stock_Back.BLL.Services;

namespace Stock_Back.Controllers.ClientApiControllers
{
    public class ClientsResponseController
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ResponseService _responseService;
        private readonly ClientService _clientService;

        public ClientsResponseController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _responseService = new ResponseService();
            _clientService = new ClientService(context, _mapper);
        }

        public async Task<IActionResult> GetResponseClients(string value, object? identifier)
        {
            Identifier ident = GetEnumValue(identifier);
            var client = await _clientService.GetClients(value, ident);
            if (client == null)
            {
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(
                $"Client with {identifier} {value} not found."));
            }
            return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(client, "Success when searching for clients"));
        }

        public async Task<ClientDTO> GetClientbyId(int id)
        {
            var clientGetter = new ClientService(_context, _mapper);
            var client = await clientGetter.GetClientById(id.ToString());
            return client;
        }

        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _clientService.DeleteClientById(id);
            if (!isDeleted)
            {
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse($"Client with id {id} not found"));

            }
            return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse($"Client with ID {id} deleted successfully", "Delete completed"));

        }

        public async Task<IActionResult> Insert(ClientInsertDTO client)
        {
            var dataModified = await _clientService.AddClient(client);

            if (dataModified > 0)
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(dataModified, $"Client with Email {client.Email} created succesfully"));
            else if (dataModified < 0)
                return _responseService.CreateResponse(ApiResponse<object>.BadRequest(client, $"Client with Email {client.Email} already exists"));
            return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse("Error trying to create a Client"));

        }

        public async Task<IActionResult> Update(ClientEditDTO clientEdited)
        {
            var (isUpdated, isClient) = await _clientService.UpdateClient(clientEdited);

            if (isUpdated)
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse($"Client with ID {clientEdited.Id} updated", "Update completed"));
            else if (!isClient)
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse($"User with ID {clientEdited.Id} not found."));
            return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse("Error trying to update User"));
        }

        private static Identifier GetEnumValue(object? value)
        {
            try
            {
                if (value is int id)
                {
                    return (Identifier)id;
                }
                else if (value is string str)
                {
                    return (Identifier)Enum.Parse(typeof(Identifier), str, true);
                }
                else
                {
                    return Identifier.Unknown;
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid identifier provided.");
            }
            
        }
    }
}
