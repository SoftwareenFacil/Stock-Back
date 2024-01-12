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
            var userGetter = new UserGetByEmail(_context);
            var user = await userGetter.GetUserByEmail(credentials.Email);
            var hasher = new Hasher();
            if (user != null && hasher.VerifyPassword(credentials.Password, user.Password))
            {
                var token = manejoJwt.GenerarToken(user.Name, user.Email, user.SuperAdmin);
                return token;
            }

            return null;
        }
    }
}