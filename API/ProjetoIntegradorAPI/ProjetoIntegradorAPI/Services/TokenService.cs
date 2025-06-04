using Microsoft.IdentityModel.Tokens;
using ProjetoIntegradorAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjetoIntegradorAPI.Services
{
    public class TokenService
    {
        public static string GenerateToken(User user)
        {
            if (user is null)
                return string.Empty;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration.Configuration.JwtKey);
            var claims = user.GetClaims();
            var TokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(TokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public static JwtSecurityToken TranslateJwt(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.ReadJwtToken(jwt);
        }
    }
}