using FinancialCore.Common;
using FinancialCore.Enums;
using FinancialCore.Handlers;
using FinancialCore.Models;
using FinancialCore.Requests.Transactions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FinancialApp.Pages.Transactions;

public class GetAllTransactionsPage : ComponentBase
{
    #region Property

    public bool IsBusy { get; set; } = false;
    public List<Transaction> Transactions { get; set; } = new();

    public GetTransactionByPeriodRequest InputModel { get; set; } = new()
    {
        EndDate = DateTime.Now.GetLastDay(),
        StartDate = DateTime.Now.GetFirstDay()
    };

    #endregion

    #region Services

    [Inject] public ITransactionHandler Handler { get; set; } = null!;

    [Inject] public NavigationManager Navigation { get; set; } = null!;

    [Inject] public IDialogService Dialog { get; set; } = null!;

    [Inject] public ISnackbar Snackbar { get; set; } = null!;

    [Inject] private ILogger<GetAllTransactionsPage> Logger { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async void OnInitialized()
    {
        IsBusy = true;
        try
        {
            FetchTransactions();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion

    #region Methods

    public async void FetchTransactions()
    {
        var response = await Handler.GetByPeriodAsync(InputModel);

        if (response.Data == null)
        {
            Snackbar.Add("Nenhuma transação encontrada no periodo.");
            return;
        }

        Transactions = response.Data;

        StateHasChanged();
    }

    public async void OnClickDeleteButton(Guid id)
    {
        var dialogResponse = await Dialog.ShowMessageBox(
            title: "Atenção!",
            cancelText: "Cancelar",
            yesText: "Excluir",
            message: "Ao prosseguir, a transação será excluida. Tem certeza disso?"
        );

        if (dialogResponse is true)
        {
            await DeleteTransition(id);
        }
    }

    private async Task DeleteTransition(Guid id)
    {
        var req = new DeleteTransactionRequest
        {
            Id = id
        };

        var response = await Handler.DeleteAsync(req);

        if (response.IsSuccess)
        {
            Transactions.RemoveAll(t => t.Id == id);
            Snackbar.Add(response.Message, Severity.Success);
        }
        else
        {
            Snackbar.Add(response.Message, Severity.Error);
        }

        StateHasChanged();
    }

    public string StylingAmountByType(ETransactionType type)
    {
        return (int)type == 0 ? "color: #26b050" : "color: #b32121";
    }

    #endregion
}