using Microsoft.AspNetCore.Mvc;
using Stock_Back.BLL.Models;
using Stock_Back.BLL.Controllers.LoginControllers;
using Stock_Back.UserJwt;
using Stock_Back.BLL.Controllers.JwtControllers;
using Stock_Back.DAL.Context;

namespace Stock_Back.Controllers
{
    public class AuthUser : ControllerBase
    {
        private AppDbContext _context;
        private IManejoJwt _manejoJwt;
        public AuthUser(IManejoJwt manejoJwt, AppDbContext context)
        {
            _context = context;
            _manejoJwt = manejoJwt;
        }

        public async Task<IActionResult> Authenticate(UserCredentials credentials)
        {
            ResponseType type = ResponseType.Failure;
            if (credentials == null || string.IsNullOrWhiteSpace(credentials.Email) || string.IsNullOrWhiteSpace(credentials.Password))
            {
                return BadRequest(ResponseHandler.GetAppResponse(type, "Invalid Credentials."));
            }
            var loger = new AuthController(_context);
            var token = await loger.Authenticate(_manejoJwt, credentials);
            if (token != null)
            {
                type = ResponseType.Success;
                return Ok(ResponseHandler.GetAppResponse(type, token));

            }
            return Unauthorized(ResponseHandler.GetAppResponse(type, "Unauthorized Credentials"));
        }
    }
}
