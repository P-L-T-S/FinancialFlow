using FinancialCore.Enums;
using FinancialCore.Handlers;
using FinancialCore.Models;
using FinancialCore.Requests.Transactions;
using FinancialCore.Responses;
using FinancialFlow.Common.Api;

namespace FinancialFlow.Endpoints.Transactions;

public class CreateTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder route)
    {
        route.MapPost("/", HandleAsync)
            .Produces<Response<Transaction?>>();
    }

    private static async Task<IResult> HandleAsync(
        ITransactionHandler handler,
        CreateTransactionRequest request
    )
    {
        request.UserId = ApiConfiguration.UserId;
        var response = await handler.CreateAsync(request);

        return response.StatusCode switch
        {
            EHttpStatusCode.Created => TypedResults.Created("", response),
            EHttpStatusCode.BadRequest => TypedResults.BadRequest(response),
            EHttpStatusCode.NotFound => TypedResults.NotFound(response),
            _ => TypedResults.BadRequest()
        };
    }
}