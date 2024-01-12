using Microsoft.AspNetCore.Mvc;
using Stock_Back.BLL.Controllers.UserControllers;
using Stock_Back.DAL.Context;
using Stock_Back.Models;
using Stock_Back.Controllers.Services;

namespace Stock_Back.Controllers.UserApiControllers
{
    public class DeleteUser : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ResponseService _responseService;
        public DeleteUser(AppDbContext context)
        {
            _context = context;
            _responseService = new ResponseService();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var deleter = new DeleteUsersController(_context);
            var isDeleted = await deleter.DeleteUserById(id);
            if (!isDeleted)
            {
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse($"User with id {id} not found"));

            }
            return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse($"User with ID {id} deleted successfully", "Delete completed"));

        }
    }
}