using Microsoft.AspNetCore.Mvc;
using Stock_Back.DAL.Interfaces;
using Stock_Back.Models;

namespace Stock_Back.Controllers.UserApiControllers
{
    public class UserGetAll : ControllerBase
    {
        private readonly IUserController _userController;
        public UserGetAll(IUserController dbController)
        {
            _userController = dbController;
        }
        
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                ResponseType type = ResponseType.Success;
                var users = await _userController.GetAllUsers();
                if (!users.Any())
                {
                    type = ResponseType.NotFound;
                    return NotFound(ResponseHandler.GetAppResponse(type, users));
                }
                return Ok(ResponseHandler.GetAppResponse(type, users));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseHandler.GetExceptionResponse(ex)); // Internal Server Error
            }
        }
    }
}
