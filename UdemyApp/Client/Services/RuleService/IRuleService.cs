namespace UdemyApp.Client.Services.RuleService
{
    public interface IRuleService
    {
        Task CreateRule(Rule rule);
        Task<List<Rule>> GetRules();
        Task<ServiceResponse<Rule>> GetRuleById(int id);
        Task<Rule> UpdateRule(Rule rule);
        Task DeleteRule(Rule rule);
    }
}
