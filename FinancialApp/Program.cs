using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FinancialApp;
using FinancialApp.Handlers;
using FinancialCore;
using FinancialCore.Handlers;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services
    .AddHttpClient(WebConfiguration.HttpClientName,
        client => { client.BaseAddress = new Uri(Configuration.BackendUrl); });

builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();

builder.Services.AddLogging();

await builder.Build().RunAsync();