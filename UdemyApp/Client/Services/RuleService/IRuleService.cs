namespace UdemyApp.Client.Services.RuleService
{
    public interface IRuleService
    {
        Task CreateRule(Rule rule);
        Task<List<Rule>> GetRules();
        Task<Rule> GetRuleById(int id);
        Task UpdateRule(Rule rule);
    }
}
