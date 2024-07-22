using FinancialCore.Enums;
using FinancialCore.Handlers;
using FinancialCore.Models;
using FinancialCore.Requests.Categories;
using FinancialCore.Responses;
using FinancialFlow.Common.Api;

namespace FinancialFlow.Endpoints.Categories;

public class UpdateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder route)
    {
        route.MapPut("/{id}", HandleAsync)
            .Produces<Response<Category?>>();
    }

    private static async Task<IResult> HandleAsync(
        ICategoryHandler handler,
        Guid id,
        string title,
        string description
    )
    {
        var request = new UpdateCategoryRequest
        {
            UserId = ApiConfiguration.UserId,
            Id = id,
            Title = title,
            Description = description
        };

        var response = await handler.UpdateAsync(request);

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