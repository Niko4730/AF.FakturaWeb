using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace UdemyApp.Client.Services.MailService
{
    public class MailService : IMailService
    {
        private readonly HttpClient _http;

        public MailService(HttpClient http)
        {
            _http = http;
        }
        public async Task<ServiceResponse<string>> SendEmail(MailDto request)
        {
            var result = await _http.PostAsJsonAsync("api/Mail/send", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }
    }
}
