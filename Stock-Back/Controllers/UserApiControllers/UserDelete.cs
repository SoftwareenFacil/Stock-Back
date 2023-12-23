using Microsoft.AspNetCore.Mvc;
using Stock_Back.DAL.Interfaces;
using Stock_Back.Models;

namespace Stock_Back.Controllers.UserApiControllers
{
    public class UserDelete : ControllerBase
    {
        private readonly IUserController _userController;
        public UserDelete(IUserController dbController)
        {
            _userController = dbController;
        }

        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                var deleted = await _userController.DeleteUser(id);
                if (!deleted)
                {
                    type = ResponseType.NotFound;
                    return NotFound(ResponseHandler.GetAppResponse(type, $"User with ID {id} not found."));
                }
                return Ok(ResponseHandler.GetAppResponse(type, $"User with ID {id} deleted successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseHandler.GetExceptionResponse(ex)); // Internal Server Error
            }
        }
    }
}