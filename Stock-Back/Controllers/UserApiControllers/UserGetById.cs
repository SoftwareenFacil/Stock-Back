using Microsoft.AspNetCore.Mvc;
using Stock_Back.DAL.Interfaces;
using Stock_Back.Models;

namespace Stock_Back.Controllers.UserApiControllers
{
    public class UserGetById : ControllerBase
    {
        private readonly IUserController _userController;
        public UserGetById(IUserController dbController)
        {
            _userController = dbController;
        }

        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                var user = await _userController.GetUserById(id);
                if (user == null)
                {
                    type = ResponseType.NotFound;
                    return NotFound(ResponseHandler.GetAppResponse(type, $"User with id {id} not found."));
                }
                return Ok(ResponseHandler.GetAppResponse(type, user));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseHandler.GetExceptionResponse(ex)); // Internal Server Error
            }
        }
    }
}
