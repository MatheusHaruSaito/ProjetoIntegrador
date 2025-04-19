using ProjetoIntegradorAPI.DTOs.UserDto;

namespace ProjetoIntegradorAPI.Repositories
{
    public class AuthenticateRepository : IAuthenticateRepository
    {
        public Task<LoginUserDto> LogIn(LoginUserDto user)
        {
            throw new NotImplementedException();
        }
    }
}
