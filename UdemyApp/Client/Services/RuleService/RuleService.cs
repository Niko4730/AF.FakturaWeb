using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace UdemyApp.Client.Services.RuleService
{
    public class RuleService : IRuleService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        private readonly AuthenticationStateProvider _authStateProvider;

        public RuleService(HttpClient http, NavigationManager navigationManager, AuthenticationStateProvider authStateProvider)
        {
            _http = http;
            _navigationManager = navigationManager;
            _authStateProvider = authStateProvider;
        }
        public List<Rule> Rules { get; set; } = new List<Rule>();

        public async Task CreateRule(Rule rule)
        {
            if(await IsUserAuthenticated())
            {
                await _http.PostAsJsonAsync("api/rule/add", rule);
            }
            else
            {
                _navigationManager.NavigateTo("login");
            }
        }

        public async Task DeleteRule(Rule rule)
        {
            var result = await _http.DeleteAsync($"api/rule/{rule.Id}");            
        }
        

        public async Task<ServiceResponse<Rule>> GetRuleById(int id)
        {

            var result = await _http.GetFromJsonAsync<ServiceResponse<Rule>>($"api/rule/{id}");
            return result;
                      
  
        }

        public async Task<List<Rule>> GetRules()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Rule>>>("api/rule");
            return result.Data;
        }

        public async Task<Rule> UpdateRule(Rule rule)
        {
            var result = await _http.PutAsJsonAsync($"api/rule/{rule.Id}", rule);
            var content = (await result.Content.ReadFromJsonAsync<ServiceResponse<Rule>>());
            return content.Data;
        }

        private async Task<bool> IsUserAuthenticated()
        {
            return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }
        private async Task SetRules(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Rule>>();
            Rules = response;
            _navigationManager.NavigateTo("rules");
        }
    }
}
