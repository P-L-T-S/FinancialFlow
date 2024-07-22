using FinancialCore;
using FinancialCore.Enums;
using FinancialCore.Handlers;
using FinancialCore.Models;
using FinancialCore.Requests.Categories;
using FinancialFlow.Common.Api;

namespace FinancialFlow.Endpoints.Categories;

public class GetCategoryByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder route)
    {
        route.MapGet("/{id}", HandleAsync)
            .Produces<Category?>();
    }

    private static async Task<IResult> HandleAsync(
        ICategoryHandler handler, Guid id)
    {
        var request = new GetCategoryByIdRequest
        {
            UserId = ApiConfiguration.UserId,
            Id = id
        };

        var response = await handler.GetByIdAsync(request);

        return response.StatusCode switch
        {
            EHttpStatusCode.Success => TypedResults.Ok(response),
            EHttpStatusCode.Created => TypedResults.Created("", response),
            EHttpStatusCode.BadRequest => TypedResults.BadRequest(response),
            EHttpStatusCode.NotFound => TypedResults.NotFound(response),
            _ => TypedResults.BadRequest()
        };
    }
}