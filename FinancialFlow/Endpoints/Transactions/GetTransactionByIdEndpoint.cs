using FinancialCore.Enums;
using FinancialCore.Handlers;
using FinancialCore.Models;
using FinancialCore.Requests.Transactions;
using FinancialCore.Responses;
using FinancialFlow.Common.Api;

namespace FinancialFlow.Endpoints.Transactions;

public class GetTransactionByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder route)
    {
        route.MapGet("/{id}", HandleAsync)
            .Produces<Response<Transaction>>();
    }

    private static async Task<IResult> HandleAsync(
        ITransactionHandler handler,
        Guid id
    )
    {
        var req = new GetTransactionByIdRequest
        {
            UserId = ApiConfiguration.UserId,
            Id = id
        };

        var response = await handler.GetByIdAsync(req);

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