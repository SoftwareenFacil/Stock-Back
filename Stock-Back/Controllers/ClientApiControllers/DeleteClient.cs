using Microsoft.AspNetCore.Mvc;
using Stock_Back.BLL.Controllers.ClientControllers;
using Stock_Back.Controllers.Services;
using Stock_Back.DAL.Context;
using Stock_Back.Models;

namespace Stock_Back.Controllers.ClientApiControllers
{
    public class DeleteClient
    {
        private readonly AppDbContext _context;
        private readonly ResponseService _responseService;
        public DeleteClient(AppDbContext context)
        {
            _context = context;
            _responseService = new ResponseService();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var deleter = new DeleteClientsController(_context);
            var isDeleted = await deleter.DeleteClientById(id);
            if (!isDeleted)
            {
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse($"Client with id {id} not found"));

            }
            return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse($"Client with ID {id} deleted successfully", "Delete completed"));

        }
    }
}
