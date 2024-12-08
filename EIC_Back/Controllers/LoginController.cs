using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EIC_Back.BLL.Models;
using EIC_Back.BLL.Controllers.JwtControllers;
using EIC_Back.DAL.Context;

namespace EIC_Back.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    
    public class LoginController : ControllerBase
    {
        private IManejoJwt manejoJwt;
        private readonly AppDbContext _context;

        public LoginController(IManejoJwt manejoJwt, AppDbContext dbContext)
        {
            this.manejoJwt = manejoJwt;
            _context = dbContext;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserCredentials credentials)
        {
            var userAuthenticator = new AuthUser(manejoJwt, _context);
            return await userAuthenticator.Authenticate(credentials);
        }
    }
}
