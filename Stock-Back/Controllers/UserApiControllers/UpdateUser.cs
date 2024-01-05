using Microsoft.AspNetCore.Mvc;
using Stock_Back.Models;
using Stock_Back.DAL.Models;
using Stock_Back.UserJwt;
using Stock_Back.DAL.Context;
using Stock_Back.DAL.Controllers.UserControllers;

namespace Stock_Back.Controllers.UserApiControllers
{
    public class UpdateUser : ControllerBase
    {
        private readonly AppDbContext _context;
        public UpdateUser(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Update(User userEdited)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                var getter = new GetUsers(_context);
                var user = await getter.GetResponseUsers(userEdited.Id);
                if (user == null)
                {
                    type = ResponseType.NotFound;
                    return NotFound(ResponseHandler.GetAppResponse(type, $"User with id {userEdited.Id} not found."));
                }
                var updater = new UserUpdate(_context);
                var updatedUser = await updater.UpdateUser(userEdited);
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
