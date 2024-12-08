using EIC_Back.BLL.Models;
using EIC_Back.DAL.Context;
using EIC_Back.BLL.Controllers.JwtControllers;
using EIC_Back.DAL.Utilities;
using EIC_Back.DAL.Repository;

namespace EIC_Back.BLL.Services
{
    public class AuthService
    {
        private AppDbContext _context;
        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string?> Authenticate(IManejoJwt manejoJwt, UserCredentials credentials)
        {
            var userGetter = new UserRepository(_context);
            var user = await userGetter.GetUserByEmail(credentials.Email);
            // var hasher = new Hasher();
            if (user != null && Hasher.VerifyPassword(credentials.Password, user.Password))
            {
                var token = manejoJwt.GenerarToken(user.Name, user.Email, user.SuperAdmin);
                return token;
            }

            return null;
        }
    }
}