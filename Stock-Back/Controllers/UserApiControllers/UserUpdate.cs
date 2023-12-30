using Microsoft.AspNetCore.Mvc;
using Stock_Back.DAL.Interfaces;
using Stock_Back.Models;
using Stock_Back.DAL.Models;

namespace Stock_Back.Controllers.UserApiControllers
{
    public class UserUpdate : ControllerBase
    {
        private readonly IUserController _userController;
        public UserUpdate(IUserController dbController)
        {
            _userController = dbController;
        }

        public async Task<IActionResult> UpdateUser(User userEdited)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                var user = await _userController.GetUserById(userEdited.Id);
                if (user == null)
                {
                    type = ResponseType.NotFound;
                    return NotFound(ResponseHandler.GetAppResponse(type, $"User with id {userEdited.Id} not found."));
                }
                var updatedUser = await _userController.UpdateUser(userEdited);
                if (updatedUser == null)
                {
                    type = ResponseType.Failure;
                    return BadRequest(ResponseHandler.GetAppResponse(type, "The name, email or password fields were not included in the request."));
                }
                return Ok(ResponseHandler.GetAppResponse(type, updatedUser));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseHandler.GetExceptionResponse(ex)); // Internal Server Error
            }
        }
    }
}
