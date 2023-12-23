using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Stock_Back.DAL.Models;
using Stock_Back.DAL.Controller;
using Stock_Back.UserJwt;
using Stock_Back.DAL.Data;
using Stock_Back.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stock_Back.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    
    public class LoginController : ControllerBase
    {
        private IManejoJwt manejoJwt;
        private UserController _userController;
        

        public LoginController(IManejoJwt manejoJwt, AppDbContext dbContext)
        {
            this.manejoJwt = manejoJwt;
            this._userController = new UserController(dbContext);
        }

        [AllowAnonymous]
        [HttpPost("Autenticar")]
        public async Task<IActionResult> Obtener([FromBody] UserCredentials credentials)
        {
            ResponseType type = ResponseType.Failure;
            // Asegúrate de que las credenciales proporcionadas sean válidas
            if (credentials == null || string.IsNullOrWhiteSpace(credentials.Email) || string.IsNullOrWhiteSpace(credentials.Password))
            {
                return BadRequest(ResponseHandler.GetAppResponse(type, "Credenciales inválidas."));
            }

            var userId = await _userController.GetUserIdByEmail(credentials.Email);
            User? user = await _userController.GetUserById(userId);
            if (user != null && user.Password == credentials.Password)
            {
                type = ResponseType.Success;
                var token = this.manejoJwt.GenerarToken(user.Email, user.Password); // Asegúrate de que manejoJwt.GenerarToken esté definido y sea el método correcto
                return Ok(ResponseHandler.GetAppResponse(type, "Token: " + token)); // Retorna el token como una respuesta 200 Ok
            }

            return Unauthorized(ResponseHandler.GetAppResponse(type,"Credencial no autorizada.")); // O podrías querer usar BadRequest o alguna otra respuesta apropiada
        }

    }
}
