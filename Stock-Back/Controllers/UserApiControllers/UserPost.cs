using Microsoft.AspNetCore.Mvc;
using Stock_Back.DAL.Interfaces;
using Stock_Back.Models;
using Stock_Back.DAL.Models;

namespace Stock_Back.Controllers.UserApiControllers
{
    public class UserPost : ControllerBase
    {
        private readonly IUserController _userController;
        public UserPost(IUserController dbController)
        {
            _userController = dbController;
        }

        public async Task<IActionResult> InsertUser(User user)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                if (await _userController.UserEmailExists(user.Email))
                {
                    type = ResponseType.Failure;
                    return BadRequest(ResponseHandler.GetAppResponse(type, "This email already exists in our records"));
                }
                var inserted = await _userController.InsertUser(user);
                if (inserted == null)
                {
                    type = ResponseType.Failure;
                    return BadRequest(ResponseHandler.GetAppResponse(type, "The user could not be inserted, please make sure you upload the correct format (name, email and password)."));
                }
                return StatusCode(201, ResponseHandler.GetAppResponse(type, inserted));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseHandler.GetExceptionResponse(ex)); // Internal Server Error
            }
        }
    }
}
