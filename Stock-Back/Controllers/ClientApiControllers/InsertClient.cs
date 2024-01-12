using Microsoft.AspNetCore.Mvc;
using Stock_Back.BLL.Controllers.ClientControllers;
using Stock_Back.BLL.Models.ClientDTO;
using Stock_Back.Controllers.Services;
using Stock_Back.DAL.Context;
using Stock_Back.Models;

namespace Stock_Back.Controllers.ClientApiControllers
{
    public class InsertClient
    {
        private readonly AppDbContext _context;
        private readonly ResponseService _responseService;
        public InsertClient(AppDbContext context)
        {
            _context = context;
            _responseService = new ResponseService();
        }

        public async Task<IActionResult> Insert(ClientInsertDTO client)
        {
            var clientCreator = new AddClientsController(_context);
            var dataModified = await clientCreator.AddClient(client);

            if (dataModified > 0)
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse($"Client with Email {client.Email} created succesfully","Create completed"));
            else if (dataModified < 0)
                return _responseService.CreateResponse(ApiResponse<object>.BadRequest(client, $"Client with Email {client.Email} already exists"));
            return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse("Error trying to create a Client"));

        }
    }
}
