using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using unolink.api.Application.Models.Request;
using unolink.api.Application.Services.AuthService;

namespace unolink.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]

        public async Task<IActionResult> Login(LogInRequest request)
        {
            var token = await _authService.CreateToken(request);
            if(token is null)
            {
                return NotFound("User Does not Exists");
            }
            return Ok(new {token = token});   
        }

    }
}
