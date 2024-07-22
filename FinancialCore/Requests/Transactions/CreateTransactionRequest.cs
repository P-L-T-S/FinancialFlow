using System.ComponentModel.DataAnnotations;
using FinancialCore.Enums;

namespace FinancialCore.Requests.Transactions;

public class CreateTransactionRequest : Request
{
    [Required(ErrorMessage = "Título inválido")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Tipo inálido")]
    public ETransactionType Type { get; set; } = ETransactionType.Withdraw;

    [Required(ErrorMessage = "Valor inválido")]
    public decimal Amount { get; set; }

    public Guid CategoryId { get; set; }

    public DateTime CreatedAt = DateTime.UtcNow.ToLocalTime();

    [Required(ErrorMessage = "Data inválida")]
    public DateTime? PaidOrReceivedAt { get; set; }
}