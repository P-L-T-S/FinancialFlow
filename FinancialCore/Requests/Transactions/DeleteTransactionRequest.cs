namespace FinancialCore.Requests.Transactions;

public class DeleteTransactionRequest: Request
{
    public Guid Id { get; set; }
}