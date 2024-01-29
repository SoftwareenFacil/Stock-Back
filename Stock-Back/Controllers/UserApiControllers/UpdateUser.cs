using Microsoft.AspNetCore.Mvc;
using Stock_Back.BLL.Models.UserDTO;
using Stock_Back.UserJwt;
using Stock_Back.BLL.Models;
using Stock_Back.BLL.Controllers.UserControllers;
using Stock_Back.DAL.Context;
using Stock_Back.Models;
using Stock_Back.Controllers.Services;

namespace Stock_Back.Controllers.UserApiControllers
{
    public class UpdateUser : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ResponseService _responseService;
        public UpdateUser(AppDbContext context)
        {
            _context = context;
            _responseService = new ResponseService();
        }

        public async Task<IActionResult> Update(UserEditDTO userEdited)
        {
            var userUpdater = new UpdateUsersController(_context);
            var code = await userUpdater.UpdateUser(userEdited);

            switch (code)
            {
                case 200:
                    return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse($"User with ID {userEdited.Id} updated", "Update completed"));
                case 404:
                    return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse($"User with ID {userEdited.Id} not found"));
                default:
                    return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse("Error trying to update User"));
            }
        }
    }
}
