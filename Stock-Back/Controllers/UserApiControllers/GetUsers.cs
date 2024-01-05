using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stock_Back.DAL.Controllers.UserControllers;
using Stock_Back.DAL.Context;
using Stock_Back.DAL.Models;
using Stock_Back.Models;
using Stock_Back.UserJwt;

namespace Stock_Back.Controllers.UserApiControllers
{
    public class GetUsers : ControllerBase
    {
        private readonly AppDbContext _context;
        public GetUsers(AppDbContext dbContext)
        {
            _context = dbContext;
            
        }

        public async Task<IActionResult> GetResponseUserById(int id)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                var _userController = new UserGetById(_context);
                var user = await _userController.GetUserById(id);
                if (user == null)
                {
                    type = ResponseType.NotFound;
                    return NotFound(ResponseHandler.GetAppResponse(type, $"User with id {id} not found."));
                }
                return Ok(ResponseHandler.GetAppResponse(type, user));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseHandler.GetExceptionResponse(ex)); // Internal Server Error
            }
        }
        public async Task<User?> GetUserByID(int id)
        {
            var use = new UserGetById(_context);
            return await use.GetUserById(id);
        }
        public async Task<int> GetUserByEmail(string email)
        {
            var use = new UserGetIdByEmail(_context);
            return await use.GetUserIdByEmail(email);
        }
    }
}
