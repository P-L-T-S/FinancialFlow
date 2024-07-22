using FinancialCore.Models;
using FinancialCore.Requests;
using FinancialCore.Requests.Transactions;
using FinancialCore.Responses;

namespace FinancialCore.Handlers;

public interface ITransactionHandler
{
    Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request);
    Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request);
    Task<Response<Transaction>> DeleteAsync(DeleteTransactionRequest request);
    Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request);
    Task<PagedResponse<List<Transaction>>> GetByPeriodAsync(GetTransactionByPeriodRequest request);
}