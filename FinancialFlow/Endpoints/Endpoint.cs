using FinancialFlow.Common.Api;
using FinancialFlow.Endpoints.Categories;
using FinancialFlow.Endpoints.Transactions;

namespace FinancialFlow.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");

        endpoints.MapGroup("/")
            .WithTags("Health Check")
            .MapGet("/", () => new { message = "ok" });

        endpoints.MapGroup("v1/categories")
            .WithTags("Categories")
            .MapEndpoint<CreateCategoryEndpoint>()
            .MapEndpoint<DeleteCategoryEndpoint>()
            .MapEndpoint<UpdateCategoryEndpoint>()
            .MapEndpoint<GetCategoryByIdEndpoint>()
            .MapEndpoint<GetAllCategoriesEndpoint>();

        endpoints.MapGroup("v1/transactions")
            .WithTags("Transactions")
            .MapEndpoint<CreateTransactionEndpoint>()
            .MapEndpoint<DeleteTransactionEndpoint>()
            .MapEndpoint<UpdateTransactionEndpoint>()
            .MapEndpoint<GetAllTransactionsEndpoint>()
            .MapEndpoint<GetTransactionByIdEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder route)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(route);
        return route;
    }
}