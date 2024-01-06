using Stock_Back.BLL.Models;
using Stock_Back.DAL.Context;
using Stock_Back.DAL.Controllers.UserControllers;
using Stock_Back.BLL.Controllers.JwtControllers;

namespace Stock_Back.BLL.Controllers.LoginControllers
{
    public class AuthController
    {
        private AppDbContext _context;
        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string?> Authenticate(IManejoJwt manejoJwt,UserCredentials credentials)
        {
            //Remover uno de los metodos, generar GetUserByEmail
            var idGetter = new UserGetIdByEmail(_context);
            var userGetter = new UserGetById(_context);
            var userId = await idGetter.GetUserIdByEmail(credentials.Email);
            var user = await userGetter.GetUserById(userId);
            var hasher = new Hasher();
            if (user != null && hasher.VerifyPassword(credentials.Password, user.Password))
            {
                var token = manejoJwt.GenerarToken(user.Email, user.SuperAdmin);
                return token;
            }

            return null;
        }
    }
}