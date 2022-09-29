using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UdemyApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;

        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }
        [HttpPost("send")]
        public async Task<ActionResult<ServiceResponse<string>>> SendEmailAsync([FromBody] MailDto request)
        {
            await _mailService.SendEmailAsync(request);
            return Ok();
        }
        [HttpPost("welcome")]
        public async Task<IActionResult> SendWelcomeMail (WelcomeMail request)
        {
            await _mailService.SendWelcomeMailAsync(request);
            return Ok();
        }
    }
}
