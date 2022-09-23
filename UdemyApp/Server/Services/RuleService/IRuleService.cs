namespace UdemyApp.Server.Services.RuleService
{
    public interface IRuleService
    {
        List<Rule> Rules { get; set; }
        Task<ServiceResponse<List<Rule>>> GetAllRules();
        Task<ServiceResponse<Rule>> GetRuleById(int id);
        Task<ServiceResponse<Rule>> CreateRule(Rule rule);
        Task<ServiceResponse<bool>> DeleteRule(int id);
        Task<ServiceResponse<Rule>> UpdateRule(Rule rule);
    }
}
