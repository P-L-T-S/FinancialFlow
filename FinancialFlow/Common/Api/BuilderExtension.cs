﻿using FinancialCore;
using FinancialCore.Handlers;
using FinancialFlow.Data;
using FinancialFlow.Handlers;
using Microsoft.EntityFrameworkCore;

namespace FinancialFlow.Common.Api;

public static class BuilderExtension
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        ApiConfiguration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
        Configuration.FrontendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
    }

    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(x => x.CustomSchemaIds(t => t.FullName));
    }

    public static void AddDataContext(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddDbContext<AppDataContext>(
                x => { x.UseSqlServer(ApiConfiguration.ConnectionString); });
    }

    public static void AddCorsOrigin(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(
            options => options.AddPolicy(
                ApiConfiguration.CorsPolicyName,
                policy => policy
                    .WithOrigins(
                        Configuration.BackendUrl,
                        Configuration.FrontendUrl
                    )
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            )
        );
    }

    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();

        builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();
    }
}