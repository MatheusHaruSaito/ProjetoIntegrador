using ProjetoIntegradorAPI.DTOs.UserDto;
using ProjetoIntegradorAPI.Models;
using System.IdentityModel.Tokens.Jwt;

namespace ProjetoIntegradorAPI.Repositories.AuthRepository
{
    public interface IAuthRepository
    {
        Task<string> LogIn(LoginUserDto user);
        JwtSecurityToken GetLoggedUser(string jwt);
    }
}
