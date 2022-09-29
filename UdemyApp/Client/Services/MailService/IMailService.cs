namespace UdemyApp.Client.Services.MailService
{
    public interface IMailService
    {
        Task<ServiceResponse<string>> SendEmail(MailDto request);
    }
}
