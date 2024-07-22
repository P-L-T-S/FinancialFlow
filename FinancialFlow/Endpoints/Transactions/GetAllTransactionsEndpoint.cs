using FinancialCore;
using FinancialCore.Enums;
using FinancialCore.Handlers;
using FinancialCore.Models;
using FinancialCore.Requests.Transactions;
using FinancialCore.Responses;
using FinancialFlow.Common.Api;
using Microsoft.AspNetCore.Mvc;

namespace FinancialFlow.Endpoints.Transactions;

public class GetAllTransactionsEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder route)
    {
        route.MapGet("/", HandleAsync)
            .Produces<PagedResponse<List<Transaction>>>();
    }

    private static async Task<IResult> HandleAsync(
        ITransactionHandler handler,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize
    )
    {
        var request = new GetTransactionByPeriodRequest
        {
            UserId = ApiConfiguration.UserId,
            PageNumber = pageNumber,
            PageSize = pageSize,
            StartDate = startDate,
            EndDate = endDate
        };

        var response = await handler.GetByPeriodAsync(request);

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