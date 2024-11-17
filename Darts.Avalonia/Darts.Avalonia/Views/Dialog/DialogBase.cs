using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace Darts.Avalonia.Views.Dialog;

public abstract class DialogBase<T> : UserControl where T : ObservableObject
{
    private readonly TaskCompletionSource<DialogResult> closeDialog = new();

    public T ViewModel { get; }

    public DialogBase(T viewModel)
    {
        ViewModel = viewModel;
    }

    public Task<DialogResult> Show()
    {
        return closeDialog.Task;
    }

    public void Close(DialogResult result)
    {
        closeDialog.SetResult(result);
    }
}
