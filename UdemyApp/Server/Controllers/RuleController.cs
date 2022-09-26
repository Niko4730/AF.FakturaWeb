using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UdemyApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuleController : ControllerBase
    {
        private readonly IRuleService _ruleservice;

        public RuleController(IRuleService ruleservice)
        {
            _ruleservice = ruleservice;
        }
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Rule>>>> GetAllRules()
        {
            var result = await _ruleservice.GetAllRules();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Rule>>> GetRuleById(int id)
        {
            var result = await _ruleservice.GetRuleById(id);
            return Ok(result);
        }
        [HttpPost("add")]
        public async Task<ActionResult<ServiceResponse<Rule>>> CreateRule(Rule rule)
        {
            var result = await _ruleservice.CreateRule(rule);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<Rule>>> UpdateRule(Rule rule)
        {
            var result = await _ruleservice.UpdateRule(rule);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteRule(int id)
        {
            var result = await _ruleservice.DeleteRule(id);
            return Ok(result);
        }
    }
}
