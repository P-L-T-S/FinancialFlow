namespace FinancialCore.Models;

public class Category
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow.ToLocalTime();
}