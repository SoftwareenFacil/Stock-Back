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

        public async Task<(string, string)> Authenticate(IManejoJwt manejoJwt, UserCredentials credentials)
        {
            var userGetter = new UserRepository(_context);
            var user = await userGetter.GetUserByEmail(credentials.Email);
            if (user != null && Hasher.VerifyPassword(credentials.Password, user.Password))
            {
                var token = manejoJwt.GenerarToken(user.Name, user.Email, user.SuperAdmin);
                var NewRefreshToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                userGetter.SaveRefresh(NewRefreshToken, user);
                return (token, NewRefreshToken);
            }

            return (null, null);
        }

        public async Task<(string,string)> Refresh(IManejoJwt manejoJwt, string refreshtoken)
        {
            var userGetter = new UserRepository(_context);
            var user = await userGetter.GetUserByRefreshToken(refreshtoken, DateTime.UtcNow);
            if (user != null)
            {
                var token = manejoJwt.GenerarToken(user.Name, user.Email, user.SuperAdmin);
                var NewRefreshToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                userGetter.SaveRefresh(NewRefreshToken, user);
                return (token,NewRefreshToken);
            }

            return (null,null);
        }
    }
}