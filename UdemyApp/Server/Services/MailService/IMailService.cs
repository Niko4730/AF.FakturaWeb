using UdemyApp.Shared;

namespace UdemyApp.Server.Services.MailService
{
    public interface IMailService
    {
        Task SendEmailAsync(MailDto request);
        Task SendWelcomeMailAsync(WelcomeMail request);
    }
}
