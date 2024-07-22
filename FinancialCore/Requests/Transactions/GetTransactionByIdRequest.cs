namespace FinancialCore.Requests.Transactions;

public class GetTransactionByIdRequest: Request
{
    public Guid Id { get; set; }
}