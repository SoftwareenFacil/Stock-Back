using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Stock_Back.BLL.Models;
using Stock_Back.Controllers.LoginControllers;
using Stock_Back.BLL.Controllers.JwtControllers;
using Stock_Back.DAL.Context;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stock_Back.Controllers
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
