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
                var user = await userGetter.GetUsers(id);
                if (user == null)
                {
                    type = ResponseType.NotFound;
                    if (id == 0)
                        return NotFound(ResponseHandler.GetAppResponse(type, $"There are no users."));
                    return NotFound(ResponseHandler.GetAppResponse(type, $"User with id {id} not found."));

                }
                return Ok(ResponseHandler.GetAppResponse(type, user));

            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
