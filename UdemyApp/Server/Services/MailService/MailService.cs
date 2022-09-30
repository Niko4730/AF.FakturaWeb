using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace UdemyApp.Server.Services.MailService
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _config;

        public MailService(IConfiguration config)
        {
            _config = config;
        }
        public async Task<ServiceResponse<string>> SendEmailAsync(MailDto request)
        {
            var response = new ServiceResponse<string>();
            var mail = new MimeMessage();
            mail.From.Add(MailboxAddress.Parse(_config.GetSection("MailSettings:EmailUsername").Value));
            mail.To.Add(MailboxAddress.Parse(request.To));
            mail.Subject = request.Subject;
            mail.Body = new TextPart(TextFormat.Html) { Text = request.Body };
            var builder = new BodyBuilder();
            if (request.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in request.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = request.Body;
            mail.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("MailSettings:EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("MailSettings:EmailUsername").Value, _config.GetSection("MailSettings:EmailPassword").Value);
            await smtp.SendAsync(mail);
            smtp.Disconnect(true);
            response.Message = "You have sent an email";
            return response;

        }

        public async Task SendWelcomeMailAsync(WelcomeMail request)
        {
            string filePath = Directory.GetCurrentDirectory() + "\\Templates\\MailTemplates\\WelcomeTemplate.html";
            StreamReader str = new StreamReader(filePath);
            string mailText = str.ReadToEnd();
            str.Close();
            mailText = mailText.Replace("[username]", request.UserName).Replace("[email]", request.To);
            var mail = new MimeMessage();
            mail.From.Add(MailboxAddress.Parse(_config.GetSection("MailSettings:EmailUsername").Value));
            mail.To.Add(MailboxAddress.Parse(request.To));
            mail.Subject = $"Welcome {request.UserName}";
            var builder = new BodyBuilder();
            builder.HtmlBody = mailText;
            mail.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("MailSettings:EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("MailSettings:EmailUsername").Value, _config.GetSection("MailSettings:EmailPassword").Value);
            await smtp.SendAsync(mail);
            smtp.Disconnect(true);
        }
    }
}
