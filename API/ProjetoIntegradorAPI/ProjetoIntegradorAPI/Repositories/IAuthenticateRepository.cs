using ProjetoIntegradorAPI.DTOs.UserDto;

namespace ProjetoIntegradorAPI.Repositories
{
    public interface IAuthenticateRepository
    {
        public Task<LoginUserDto> LogIn(LoginUserDto user);
    }
}
