using FinancialCore.Enums;
using FinancialCore.Handlers;
using FinancialCore.Models;
using FinancialCore.Requests.Categories;
using FinancialCore.Responses;
using FinancialFlow.Common.Api;
using FinancialFlow.Handlers;

namespace FinancialFlow.Endpoints.Categories;

public class CreateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder route)
    {
        route.MapPost("/", HandlerAsync)
            .WithName("Categories: create")
            .WithSummary("Crie uma nova categoria")
            .WithDescription("Crie uma nova categoria")
            .WithOrder(1)
            .Produces<Response<Category>>();
    }


    private static async Task<IResult> HandlerAsync(
        ICategoryHandler handler, CreateCategoryRequest request
    )
    {
        request.UserId = ApiConfiguration.UserId;
        var response = await handler.CreateAsync(request);

        return response.StatusCode switch
        {
            EHttpStatusCode.Success => TypedResults.Ok(response),
            EHttpStatusCode.Created => TypedResults.Created($"v1/categories/{response.Data?.Id}", response),
            EHttpStatusCode.BadRequest => TypedResults.BadRequest(response),
            EHttpStatusCode.NotFound => TypedResults.NotFound(response),
            _ => TypedResults.BadRequest()
        };
    }
}