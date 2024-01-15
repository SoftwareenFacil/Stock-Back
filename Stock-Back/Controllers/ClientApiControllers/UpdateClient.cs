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
            var (isUpdated, isClient) = await clientUpdater.UpdateClient(clientEdited);

            if (isUpdated)
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse($"Client with ID {clientEdited.Id} updated","Update completed"));
            else if (!isClient)
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse($"User with ID {clientEdited.Id} not found."));
            return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse("Error trying to update User"));
        }
    }
}
