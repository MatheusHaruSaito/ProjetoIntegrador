
using MailKit.Net.Smtp;
using MimeKit;
using unolink.api.Application.Models.Request;


namespace unolink.api.Application.Services.EmailService
{
    public class EmailService : IEmailService
    {
        public async Task EmailSender(CreateEmailRequest request)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("UnoLink", "unolinkcontato@gmail.com"));
            message.To.Add(new MailboxAddress("", request.ToEmail));
            message.Subject = request.Subject;
            message.Body = new TextPart("plain")
            {
                Text = request.Body
            };

            using(var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync("unolinkcontato@gmail.com", "ijvi yhme yghy bbhf");
                await smtp.SendAsync(message);
                await smtp.DisconnectAsync(true);
            }
        }
    }
}
