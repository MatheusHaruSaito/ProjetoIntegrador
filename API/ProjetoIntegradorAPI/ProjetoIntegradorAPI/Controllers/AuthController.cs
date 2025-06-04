using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ProjetoIntegradorAPI.DTOs.UserDto;
using ProjetoIntegradorAPI.Repositories.AuthRepository;
using ProjetoIntegradorAPI.Services;
using System.IdentityModel.Tokens.Jwt;

namespace ProjetoIntegradorAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(LoginUserDto loginUser)
        {
            try
            {
                string Token = await _authRepository.LogIn(loginUser);
                if (Token == string.Empty) return BadRequest("User nom Existent");
                return Ok(new { token = Token });
            }
            catch
            {
                return BadRequest("Could not create the login Token");
            }
        }
        [HttpGet("{jwt}")]
        public  ActionResult<JwtSecurityToken> GetUserFromToken(string jwt)
        {
            JwtSecurityToken token = _authRepository.GetLoggedUser(jwt);
            return Ok(token.Claims);
        }
    }
}
