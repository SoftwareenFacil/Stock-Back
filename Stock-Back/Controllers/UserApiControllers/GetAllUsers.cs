using Microsoft.AspNetCore.Mvc;
using Stock_Back.DAL.Controller.UserControllers;
using Stock_Back.DAL.Data;
using Stock_Back.DAL.Interfaces;
using Stock_Back.Models;
using Stock_Back.UserJwt;

namespace Stock_Back.Controllers.UserApiControllers
{
    public class GetAllUsers : ControllerBase
    {
        private readonly UserGetAll _users;
        public GetAllUsers(AppDbContext context)
        {
            _users = new UserGetAll(context);
        }
        
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                ResponseType type = ResponseType.Success;
                var users = await _users.GetAllUsers();
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
