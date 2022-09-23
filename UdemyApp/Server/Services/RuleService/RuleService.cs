using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UdemyApp.Shared;

namespace UdemyApp.Server.Services.RuleService
{
    public class RuleService : IRuleService
    {
        private readonly MainDbContext _context;

        public RuleService(MainDbContext context)
        {
            _context = context;
        }

        public List<Rule> Rules { get; set; }

        public async Task<ServiceResponse<Rule>> CreateRule(Rule rule)
        {
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
            var response = new ServiceResponse<List<Rule>>()
            {
                Data = await _context.Rules.ToListAsync()
            };

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
