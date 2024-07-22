using System.Net.Http.Json;
using FinancialCore.Enums;
using FinancialCore.Handlers;
using FinancialCore.Models;
using FinancialCore.Requests.Transactions;
using FinancialCore.Responses;

namespace FinancialApp.Handlers;

public class TransactionHandler : ITransactionHandler
{
    private readonly HttpClient _http;
    private readonly ILogger<CategoryHandler> _logger;

    public TransactionHandler(IHttpClientFactory http, ILogger<CategoryHandler> logger)
    {
        _http = http.CreateClient(WebConfiguration.HttpClientName);
        _logger = logger;
    }

    public Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<Transaction>> DeleteAsync(DeleteTransactionRequest request)
    {
        try
        {
            var response = await _http.DeleteFromJsonAsync<Response<Transaction>>($"v1/transactions/{request.Id}");

            if (response is null)
            {
                return new Response<Transaction>(
                    null,
                    EHttpStatusCode.BadRequest,
                    "Erro ao buscar categoria"
                );
            }

            return response;
        }
        catch (Exception e)
        {
            return new Response<Transaction>(
                null,
                EHttpStatusCode.BadRequest,
                e.Message
            );
        }
    }

    public Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<PagedResponse<List<Transaction>>> GetByPeriodAsync(GetTransactionByPeriodRequest request)
    {
        try
        {
            var response = await _http.GetFromJsonAsync<PagedResponse<List<Transaction>>>("v1/transactions");

            if (response == null)
            {
                return new PagedResponse<List<Transaction>>(
                    null,
                    EHttpStatusCode.BadRequest,
                    "Erro ao buscar categoria"
                );
            }

            return response;
        }
        catch (Exception e)
        {
            return new PagedResponse<List<Transaction>>(
                null,
                EHttpStatusCode.BadRequest,
                e.Message
            );
        }
    }
}