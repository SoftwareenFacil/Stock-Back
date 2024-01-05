using Microsoft.AspNetCore.Mvc;
using Stock_Back.BLL.Controllers.UserControllers;
using Stock_Back.DAL.Context;
using Stock_Back.BLL.Models;
using Stock_Back.UserJwt;

namespace Stock_Back.Controllers.UserApiControllers
{
    public class DeleteUser : ControllerBase
    {
        private readonly AppDbContext _context;
        public DeleteUser(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Delete(int id, string? isSuperAdminClaim)
        {
            if (bool.Parse(isSuperAdminClaim))
            {
                try
                {
                    ResponseType type = ResponseType.Success;
                    var deleter = new DeleteUsersController(_context);
                    var isDeleted = await deleter.DeleteUserById(id);
                    if (!isDeleted)
                    {
                        type = ResponseType.NotFound;
                        return NotFound(ResponseHandler.GetAppResponse(type, $"User with ID {id} not found."));
                    }
                    return Ok(ResponseHandler.GetAppResponse(type, $"User with ID {id} deleted successfully."));
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ResponseHandler.GetExceptionResponse(ex));
                }
            }
            else
            {
                return Forbid("No tienes permisos para insertar usuarios.");
            }
        }
    }
}