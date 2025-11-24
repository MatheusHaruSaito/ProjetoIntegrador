using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using unolink.api.Application.Models.Request;
using unolink.api.Application.Services.EmailService;

namespace unolink.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController(IEmailService _emailService) : ControllerBase
    {
        [HttpPost]
        public IActionResult SendEmail(CreateEmailRequest request) {
            _emailService.EmailSender(request);


            return Ok();
        }
    }
}
