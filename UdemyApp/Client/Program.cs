global using UdemyApp.Shared;
global using UdemyApp.Client.Services.AuthService;
global using Microsoft.AspNetCore.Components.Authorization;
global using UdemyApp.Client.Services.RuleService;
global using UdemyApp.Client.Services.MailService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using UdemyApp.Client;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<IRuleService, RuleService>();
builder.Services.AddScoped<IMailService, MailService>();

await builder.Build().RunAsync();
