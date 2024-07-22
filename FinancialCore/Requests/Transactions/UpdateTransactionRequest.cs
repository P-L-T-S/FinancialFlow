using System.ComponentModel.DataAnnotations;
using FinancialCore.Enums;
using FinancialCore.Models;

namespace FinancialCore.Requests.Transactions;

public class UpdateTransactionRequest : Request
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Título inválido")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Tipo inválido")]
    public ETransactionType Type { get; set; }

    public DateTime? PaidOrReceivedAt { get; set; }

    [Required(ErrorMessage = "Valor inválido")]
    public decimal Amount { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public Guid UserId { get; set; }
}