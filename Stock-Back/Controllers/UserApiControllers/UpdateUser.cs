using Microsoft.AspNetCore.Mvc;
using Stock_Back.BLL.Models.DTO;
using Stock_Back.UserJwt;
using Stock_Back.BLL.Models;
using Stock_Back.BLL.Controllers.UserControllers;
using Stock_Back.DAL.Context;

namespace Stock_Back.Controllers.UserApiControllers
{
    public class UpdateUser : ControllerBase
    {
        private readonly AppDbContext _context;
        public UpdateUser(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Update(UserEditDTO userEdited, string? isSuperAdminClaim)
        {
            if(bool.Parse(isSuperAdminClaim))
            {
                try
                {
                    var userUpdater = new UpdateUsersController(_context);
                    var type = await userUpdater.UpdateUser(userEdited);

                    if (type == ResponseType.Success)
                    {
                        return Ok(ResponseHandler.GetAppResponse(type, $"User with id {userEdited.Id} updated."));
                    } 
                    else if(type == ResponseType.NotFound)
                    {
                        return NotFound(ResponseHandler.GetAppResponse(type, $"User with id {userEdited.Id} not found."));
                    }
                    else
                    {
                        return BadRequest(ResponseHandler.GetAppResponse(type, "The name, email or password fields were not included in the request."));
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ResponseHandler.GetExceptionResponse(ex)); // Internal Server Error
                }
            }
            else
            {
                return Forbid("No tienes permisos para insertar usuarios.");
            }
        }
    }
}
