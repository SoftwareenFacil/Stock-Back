using Microsoft.AspNetCore.Mvc;
using Stock_Back.DAL.Interfaces;
using Stock_Back.Models;
using Stock_Back.DAL.Models;
using Stock_Back.UserJwt;
using Stock_Back.DAL.Context;
using Stock_Back.DAL.Controllers.UserControllers;

namespace Stock_Back.Controllers.UserApiControllers
{
    public class AddUser : ControllerBase
    {
        private readonly AppDbContext _context;
        public AddUser(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> InsertUser(User user, string? isSuperAdminClaim)
        {
            if (!string.IsNullOrEmpty(isSuperAdminClaim))
            {
                try
                {
                    ResponseType type = ResponseType.Success;
                    var getEmail = new UserGetIdByEmail(_context);
                    if (await getEmail.GetUserIdByEmail(user.Email) > 0)
                    {
                        type = ResponseType.Failure;
                        return BadRequest(ResponseHandler.GetAppResponse(type, "This email already exists in our records")); //TODO: all error messages must move to a config file
                    }
                    var inserter = new UserPost(_context);
                    var inserted = await inserter.InsertUser(user);
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
            else
            {
                // El usuario no es SuperAdmin, no permitir la acción.
                return Forbid("No tienes permisos para insertar usuarios.");
            }
        }
    }
}
