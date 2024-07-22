using FinancialCore.Handlers;
using FinancialCore.Models;
using FinancialCore.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FinancialApp.Pages.Categories;

public class GetAllCategoriesPage : ComponentBase
{
    #region Proprieties

    public bool IsBusy { get; set; } = false;

    public List<Category> Categories { get; set; } = new List<Category>();

    public List<Category> DefaultValues = new()
    {
        new()
        {
            Title = "teste",
            Id = new Guid(),
            Description = "teste"
        },
        new()
        {
            Title = "teste",
            Id = new Guid(),
            Description = "teste"
        },
        new()
        {
            Title = "teste",
            Id = new Guid(),
            Description = "teste"
        }
    };

    #endregion

    #region Services

    [Inject] public ICategoryHandler Handler { get; set; } = null!;

    [Inject] public NavigationManager Navigation { get; set; } = null!;

    [Inject] public IDialogService Dialog { get; set; } = null!;

    [Inject] public ISnackbar Snackbar { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            var request = new GetAllCategoryRequest();
            var response = await Handler.GetAllAsync(request);

            if (response.IsSuccess)
            {
                Categories = response.Data ?? new List<Category>();
            }
            else
            {
                Snackbar.Add(response.Message, Severity.Error);
            }
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion

    public async void OnClickDeleteButton(Guid id, string title)
    {
        var dialogResponse = await Dialog.ShowMessageBox(
            title: "Atenção",
            cancelText: "Cancelar",
            yesText: "Excluir",
            message: $"Ao prosseguir, a categoria {title} será removida. Tem certeza disso ?"
        );

        if (dialogResponse is true)
        {
            await OnDeleteAsync(id);
        }
    }

    public async Task OnDeleteAsync(Guid id)
    {
        try
        {
            var req = new DeleteCategoryRequest
            {
                Id = id,
            };

            var response = await Handler.DeleteAsync(req);

            if (response.IsSuccess)
            {
                Categories.RemoveAll(x => x.Id == id);

                Snackbar.Add(response.Message, Severity.Success);
            }
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }

        StateHasChanged();
    }

    #region teste

    private List<string> _events = new();

    #endregion
}