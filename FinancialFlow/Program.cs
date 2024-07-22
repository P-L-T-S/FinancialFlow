using FinancialFlow;
using FinancialFlow.Common.Api;
using FinancialFlow.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddDataContext();
builder.AddCorsOrigin();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.ConfigureDevEnvironment();
}

app.UseCors(ApiConfiguration.CorsPolicyName);
app.MapEndpoints();

app.Run();