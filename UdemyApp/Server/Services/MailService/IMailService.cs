using UdemyApp.Shared;

namespace UdemyApp.Server.Services.MailService
{
    public interface IMailService
    {       
        Task SendWelcomeMailAsync(WelcomeMail request);
        Task<ServiceResponse<string>> SendEmailAsync(MailDto request);
    }
}
