using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UdemyApp.Shared;

namespace UdemyApp.Server.Services.RuleService
{
    public class RuleService : IRuleService
    {
        private readonly MainDbContext _context;
        private readonly IAuthService _authService;

        public RuleService(MainDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        
        public List<Rule> Rules { get; set; }

        public async Task<ServiceResponse<Rule>> CreateRule(Rule rule)
        {
            var userId = _authService.GetUserId();
            new Rule
            {
                Title = rule.Title,
                Description = rule.Description,
                Occurrence = rule.Occurrence,
                UserId = rule.UserId = userId
            };
            _context.Rules.Add(rule);
            await _context.SaveChangesAsync();
            return new ServiceResponse<Rule> { Data = rule };
        }

        public async Task<ServiceResponse<bool>> DeleteRule(int id)
        {
            var dbRule = await _context.Rules.FindAsync(id);
            if(dbRule == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Rule not found"
                };
            }
            _context.Remove(dbRule);
            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }


        public async Task<ServiceResponse<List<Rule>>> GetAllRules()
        {
            var response = new ServiceResponse<List<Rule>>();
            var rules = await _context.Rules
                .Where(r => r.UserId == _authService.GetUserId())
                .ToListAsync();

            var rule = new List<Rule>();
            rules.ForEach(r => rule.Add(new Rule
            {
                Id = r.Id,
                Description = r.Description,
                Title = r.Title,
                Occurrence = r.Occurrence,
            }));
            response.Data = rule;

            return response;
                
        }

        public async Task<ServiceResponse<Rule>> GetRuleById(int id)
        {
            var response = new ServiceResponse<Rule>();
            var rule = await _context.Rules.FindAsync(id);
            if (rule == null)
            {
                response.Success = false;
                response.Message = "This rule does not exist";
            }
            else
            {
                response.Data = rule;
            }
            return response;
        }

        public async Task<ServiceResponse<Rule>> UpdateRule(Rule rule)
        {
            var dbRule = await _context.Rules.FindAsync(rule.Id);
            if (dbRule == null)
            {
                return new ServiceResponse<Rule>
                {
                    Success = false,
                    Message = "Rule not found"
                };
            }
            dbRule.Title = rule.Title;
            dbRule.Description = rule.Description;
            dbRule.Occurrence = rule.Occurrence;

            await _context.SaveChangesAsync();
            return new ServiceResponse<Rule> { Data = rule };
        }
    }
}
