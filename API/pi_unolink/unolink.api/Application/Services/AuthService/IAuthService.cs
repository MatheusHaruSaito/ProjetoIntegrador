using unolink.api.Application.Models.Dtos;
using unolink.api.Application.Models.Request;

namespace unolink.api.Application.Services.AuthService
{
    public interface IAuthService
    {
        Task<string> CreateToken(LogInRequest logInRequest);
    }
}
