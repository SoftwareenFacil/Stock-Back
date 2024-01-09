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

        public async Task<IActionResult> Update(UserEditDTO userEdited)
        {
            try
            {
                ResponseType type = ResponseType.Failure;
                var userUpdater = new UpdateUsersController(_context);
                var (isUpdated,isUser) = await userUpdater.UpdateUser(userEdited);

                if (isUpdated == true)
                {
                    type = ResponseType.Success;
                    return Ok(ResponseHandler.GetAppResponse(type, $"User with id {userEdited.Id} updated."));
                }
                
                if (isUser == false)
                {
                    type = ResponseType.NotFound;
                    return NotFound(ResponseHandler.GetAppResponse(type, $"User with id {userEdited.Id} not found."));
                }

                return BadRequest(ResponseHandler.GetAppResponse(type, "Error trying to update database"));

            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
