using Microsoft.AspNetCore.Mvc;
using Stock_Back.BLL.Controllers.ClientControllers;
using Stock_Back.BLL.Models.ClientDTO;
using Stock_Back.Controllers.Services;
using Stock_Back.DAL.Context;
using Stock_Back.Models;

namespace Stock_Back.Controllers.ClientApiControllers
{
    public class UpdateClient
    {
        private readonly AppDbContext _context;
        private readonly ResponseService _responseService;
        public UpdateClient(AppDbContext context)
        {
            _context = context;
            _responseService = new ResponseService();
        }

        public async Task<IActionResult> Update(ClientEditDTO clientEdited)
        {
            var clientUpdater = new UpdateClientsController(_context);
            var code = await clientUpdater.UpdateClient(clientEdited);

            switch (code)
            {
                case 200:
                    return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse($"Client with ID {clientEdited.Id} updated", "Update completed"));
                case 404:
                    return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse($"Client with ID {clientEdited.Id} not found."));
                default:
                    return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse("Error trying to update Client"));
            }
        }
    }
}
