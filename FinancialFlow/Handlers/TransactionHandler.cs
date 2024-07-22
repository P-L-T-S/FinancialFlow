using FinancialCore.Common;
using FinancialCore.Enums;
using FinancialCore.Handlers;
using FinancialCore.Models;
using FinancialCore.Requests;
using FinancialCore.Requests.Transactions;
using FinancialCore.Responses;
using FinancialFlow.Data;
using Microsoft.EntityFrameworkCore;

namespace FinancialFlow.Handlers;

public class TransactionHandler(AppDataContext context) : ITransactionHandler
{
    public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
    {
        if (request is
            {
                Type: ETransactionType.Withdraw,
                Amount: >= 0
            })
        {
            request.Amount *= -1;
        }

        var transaction = new Transaction
        {
            UserId = request.UserId,
            CategoryId = request.CategoryId,
            Type = request.Type,
            Amount = request.Amount,
            Title = request.Title,
            PaidOrReceivedAt = request.PaidOrReceivedAt,
        };

        try
        {
            await context.Transactions.AddAsync(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(transaction, EHttpStatusCode.Created, "Transação criada com sucesso");
        }
        catch (Exception e)
        {
            return new Response<Transaction?>(null, EHttpStatusCode.Error, "Erro ao criar transação");
        }
    }

    public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
    {
        if (request is { Type: ETransactionType.Withdraw, Amount: >= 0 })
        {
            request.Amount *= -1;
        }

        try
        {
            var transaction = await context
                .Transactions
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (transaction == null)
            {
                return new Response<Transaction?>(null, EHttpStatusCode.NotFound, "Transação não encontrada");
            }

            transaction.Title = request.Title;
            transaction.CategoryId = request.CategoryId;
            transaction.Type = request.Type;
            transaction.Amount = request.Amount;
            transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;

            context.Transactions.Update(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(transaction, EHttpStatusCode.Success, "Transação editada com sucesso");
        }
        catch (Exception e)
        {
            return new Response<Transaction?>(null, EHttpStatusCode.Error, "Erro ao editar transação");
        }
    }

    public async Task<Response<Transaction>> DeleteAsync(DeleteTransactionRequest request)
    {
        try
        {
            var transaction = await context
                .Transactions
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (transaction == null)
            {
                return new Response<Transaction?>(null, EHttpStatusCode.NotFound, "Transação não encontrada");
            }

            context.Transactions.Remove(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(null, EHttpStatusCode.Success, "Transação deletada com sucesso");
        }
        catch (Exception e)
        {
            return new Response<Transaction?>(null, EHttpStatusCode.Error, "Erro ao deletar transação");
        }
    }

    public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
    {
        try
        {
            var transaction =
                await context
                    .Transactions
                    .Include(transaction => transaction.Category)
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);


            return transaction == null
                ? new Response<Transaction?>(transaction, EHttpStatusCode.NotFound, "Transação não encontrada")
                : new Response<Transaction?>(transaction, EHttpStatusCode.Success);
        }
        catch (Exception e)
        {
            return new Response<Transaction?>(null, EHttpStatusCode.Error, "Erro ao buscar transação");
        }
    }

    public async Task<PagedResponse<List<Transaction>>> GetByPeriodAsync(GetTransactionByPeriodRequest request)
    {
        try
        {
            request.StartDate ??= DateTime.Now.GetFirstDay();
            request.EndDate ??= DateTime.Now.GetLastDay();

            var query = context
                .Transactions
                .Include(transaction => transaction.Category)
                .AsNoTracking()
                .Where(x =>
                    x.UserId == request.UserId &&
                    x.PaidOrReceivedAt >= request.StartDate &&
                    x.PaidOrReceivedAt <= request.EndDate
                )
                .OrderBy(x => x.PaidOrReceivedAt)
                .Take(request.PageSize)
                .Skip((request.PageNumber - 1) * request.PageSize);

            var count = await context.Transactions.CountAsync();

            var transactions = await query.ToListAsync();

            return new PagedResponse<List<Transaction>>(transactions, count, request.PageNumber, request.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<Transaction>>(null, EHttpStatusCode.Error, "Erro ao criar transação");
        }
    }
}