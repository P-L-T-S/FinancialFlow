using FinancialCore.Enums;

namespace FinancialCore.Models;

public class Transaction
{
    public Guid Id { get; init; }
    public string Title { get; set; } = string.Empty;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow.ToLocalTime();
    public DateTime? PaidOrReceivedAt { get; set; }

    public ETransactionType Type { get; set; } = ETransactionType.Withdraw;

    public decimal Amount { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public Guid UserId { get; set; }
}