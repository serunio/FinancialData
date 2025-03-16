using FinancialData.WebClient;
using FinancialData.WebClient.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7066/") });
builder.Services.AddScoped<RecordService>();
builder.Services.AddScoped<DataParamsService>();
builder.Services.AddBlazorBootstrap();
await builder.Build().RunAsync();
