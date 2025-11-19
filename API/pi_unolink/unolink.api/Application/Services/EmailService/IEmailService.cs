using unolink.api.Application.Models.Request;

namespace unolink.api.Application.Services.EmailService
{
    public interface IEmailService
    {
        public Task EmailSender(CreateEmailRequest request);
    }
}
