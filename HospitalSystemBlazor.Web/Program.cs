
using HospitalSystemBlazor.Web;
using HospitalSystemBlazor.Web.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using HospitalSystemBlazor.Web.Utilities;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthenticationStateProvider>());
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<EspecialidadServ>();
builder.Services.AddScoped<AuthServ>();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri("https://localhost:7010") 
});



await builder.Build().RunAsync();
