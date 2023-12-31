using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Stock_Back.DAL.Models;
using Stock_Back.DAL.Controller;
using Stock_Back.UserJwt;
using Stock_Back.DAL.Data;
using Stock_Back.Models;
using Stock_Back.Controllers.UserApiControllers;

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
        [HttpPost("Autenticar")]
        public async Task<IActionResult> Obtener([FromBody] UserCredentials credentials)
        {
            ResponseType type = ResponseType.Failure;
            // Asegúrate de que las credenciales proporcionadas sean válidas
            if (credentials == null || string.IsNullOrWhiteSpace(credentials.Email) || string.IsNullOrWhiteSpace(credentials.Password))
            {
                return BadRequest(ResponseHandler.GetAppResponse(type, "Invalid Credentials."));
            }
            var getter = new GetUsers(_context);
            var userId = await getter.GetUserByEmail(credentials.Email);
            User? user = await getter.GetUserByID(userId);
            if (user != null && user.Password == credentials.Password)
            {
                type = ResponseType.Success;
                var token = manejoJwt.GenerarToken(user.Email, user.SuperAdmin); // Asegúrate de que manejoJwt.GenerarToken esté definido y sea el método correcto
                return Ok(ResponseHandler.GetAppResponse(type, token)); // Retorna el token como una respuesta 200 Ok
            }

            //return StatusCode(401, ResponseHandler.GetAppResponse(type, "Credencial no autorizada."));
            return Unauthorized(ResponseHandler.GetAppResponse(type, "Unauthorized Credentials")); // O podrías querer usar BadRequest o alguna otra respuesta apropiada
        }
    }
}
