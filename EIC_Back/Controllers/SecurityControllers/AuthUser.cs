using Microsoft.AspNetCore.Mvc;
using EIC_Back.BLL.Models;
using EIC_Back.UserJwt;
using EIC_Back.BLL.Controllers.JwtControllers;
using EIC_Back.DAL.Context;
using EIC_Back.BLL.Services;

namespace EIC_Back.Controllers
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
            var loger = new AuthService(_context);
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
