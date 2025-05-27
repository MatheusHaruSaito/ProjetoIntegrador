using ProjetoIntegradorAPI.DTOs.UserDto;
using ProjetoIntegradorAPI.Models;

namespace ProjetoIntegradorAPI.Repositories.AuthRepository
{
    public interface IAuthRepository
    {
        Task<string> LogIn(LoginUserDto user);
    }
}
