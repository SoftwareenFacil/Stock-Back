using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EIC_Back.BLL.Controllers.JwtControllers
{
    public class ManejoJwt: IManejoJwt
    {
        public IConfiguration configuration;

        public ManejoJwt(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public string GenerarToken(string name, string email, bool superAdmin)
        {
            var claims = new List<Claim>
            {
                new Claim("name", name),
                new Claim("email", email),
                new Claim("SuperAdmin", superAdmin.ToString())
            };

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("ConfiguracionJwt:Llave").Get<string>() ?? string.Empty));
            var credentials = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration.GetSection("ConfiguracionJwt:Issuer").Get<string>() ?? string.Empty,
                audience: configuration.GetSection("ConfiguracionJwt:Audience").Get<string>() ?? string.Empty,
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
