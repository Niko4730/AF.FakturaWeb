global using Microsoft.AspNetCore.Components.Authorization;
global using UdemyApp.Client.Services.AuthService;
global using UdemyApp.Client.Services.MailService;
global using UdemyApp.Client.Services.RuleService;
global using UdemyApp.Shared;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using UdemyApp.Client;

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
builder.Services.AddMudServices();

await builder.Build().RunAsync();
