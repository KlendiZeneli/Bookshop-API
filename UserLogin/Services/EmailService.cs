using FluentEmail.Core;
using System.Threading.Tasks;

namespace UserLogin.Services
{
    public class EmailService
    {
        private readonly IFluentEmail _fluentEmail;

        public EmailService(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body, bool isHtml = false)
        {
            var email = await _fluentEmail
                .To(toEmail)
                .Subject(subject)
                .Body(body,isHtml)
                .SendAsync();

            if (!email.Successful)
            {
                throw new Exception($"Failed to send email: {string.Join(", ", email.ErrorMessages)}");
            }
        }
    }
}
