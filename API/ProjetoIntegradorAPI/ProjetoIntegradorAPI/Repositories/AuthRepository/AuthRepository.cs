using Microsoft.EntityFrameworkCore;
using ProjetoIntegradorAPI.Context;
using ProjetoIntegradorAPI.DTOs.UserDto;
using ProjetoIntegradorAPI.Models;
using ProjetoIntegradorAPI.Services;
using System.IdentityModel.Tokens.Jwt;

namespace ProjetoIntegradorAPI.Repositories.AuthRepository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDataContext _application;
        public AuthRepository(ApplicationDataContext applicationDataContext)
        {
            _application = applicationDataContext;
        }
        public async Task<string> LogIn(LoginUserDto user)
        {
            
            var DbUser = await _application.User.FirstOrDefaultAsync(u => u.Email == user.Email);
            var token = TokenService.GenerateToken(DbUser);
            if (token == string.Empty) return string.Empty;
            return token;
        }
        public  JwtSecurityToken GetLoggedUser(string jwt)
        {
            return  TokenService.TranslateJwt(jwt);
        }
    }
}
