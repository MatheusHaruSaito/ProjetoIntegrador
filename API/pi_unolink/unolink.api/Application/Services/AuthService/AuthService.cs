using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using unolink.api.Application.Models.Dtos;
using unolink.api.Application.Models.Dtos.DtoExtension;
using unolink.api.Application.Models.Request;
using unolink.api.Application.Services.UserService;
using unolink.domain.Core.Interfaces;
using unolink.domain.Models;

namespace unolink.api.Application.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private readonly IUserRepository _userService;
        public AuthService(IConfiguration config, IUserRepository userService, UserManager<User> userManager)
        {
            _userManager = userManager;
            _config = config;
            _userService = userService;
        }
        public async Task<string> CreateToken(LogInRequest logInRequest)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            var user = await _userManager.FindByEmailAsync(logInRequest.Email);

            if(user is null)
            {   
                return null;
            }

            if(!await _userManager.CheckPasswordAsync(user, logInRequest.Password))
            {
                return null;
            }

            UserClaimDTO userClaims = new()
            {
                Email = user.Email,
                Name = user.UserName,
                Role = await _userManager.GetRolesAsync(user),
                Id = user.Id,
                
            };
            var claims = userClaims.GetClaims();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpirationInMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


    }
}
