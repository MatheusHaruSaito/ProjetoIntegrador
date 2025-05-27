using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ProjetoIntegradorAPI.DTOs.UserDto;
using ProjetoIntegradorAPI.Repositories.AuthRepository;

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
        public async Task<ActionResult> Login(LoginUserDto loginUser)
        {
            try
            {
                string Token = await _authRepository.LogIn(loginUser);
                return Ok(Token);
            }
            catch
            {
                return BadRequest("Could not create the login Token");
            }
        }
    }
}
