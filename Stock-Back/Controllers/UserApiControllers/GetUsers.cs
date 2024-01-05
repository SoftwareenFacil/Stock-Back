using Microsoft.AspNetCore.Mvc;
using Stock_Back.BLL.Controllers.UserControllers;
using Stock_Back.UserJwt;
using Stock_Back.DAL.Context;
using Stock_Back.BLL.Models;

namespace Stock_Back.Controllers.UserApiControllers
{
    public class GetUsers : ControllerBase
    {
        private readonly AppDbContext _context;
        public GetUsers(AppDbContext dbContext)
        {
            _context = dbContext;
            
        }

        public async Task<IActionResult> GetResponseUsers(int id)
        {
            try
            {
                ResponseType type = ResponseType.Success; 
                var userGetter = new GetUsersController(_context);
                //Esta logica moverla a la capa de negocios
                if (id == 0)
                {
                    var users = await userGetter.GetAllUsers();
                    if (users == null)
                    {
                        type = ResponseType.NotFound;
                        return NotFound(ResponseHandler.GetAppResponse(type, $"User with id {id} not found."));
                    }
                    return Ok(ResponseHandler.GetAppResponse(type, users));
                }
                else
                {
                    var user = await userGetter.GetUserById(id);
                    if (user == null)
                    {
                        type = ResponseType.NotFound;
                        return NotFound(ResponseHandler.GetAppResponse(type, $"User with id {id} not found."));
                    }
                    return Ok(ResponseHandler.GetAppResponse(type, user));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseHandler.GetExceptionResponse(ex)); // Internal Server Error
            }
        }
    }
}
