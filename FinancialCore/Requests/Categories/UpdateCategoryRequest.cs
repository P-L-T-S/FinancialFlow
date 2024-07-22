using System.ComponentModel.DataAnnotations;

namespace FinancialCore.Requests.Categories;

public class UpdateCategoryRequest
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Título inválido")]
    [MaxLength(80, ErrorMessage = "O título deve conter até 80 caracteres")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Descrição inválida")]
    public string? Description { get; set; }

    public Guid UserId { get; set; }
}