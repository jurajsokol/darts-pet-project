using Avalonia.Controls;
using Avalonia.Interactivity;
using Darts.Avalonia.ViewModels;
using System.Threading.Tasks;

namespace Darts.Avalonia.Views.Dialog;

public partial class AddPlayerView : UserControl, IDialog<AddPlayerViewModel>
{
    private readonly TaskCompletionSource closeDialog = new();

    public AddPlayerViewModel ViewModel { get; }

    public AddPlayerView(AddPlayerViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
    }

    public Task Show()
    {
        return closeDialog.Task;
    }

    private void Close()
    {
        closeDialog.SetResult();
    }

    private void Button_Click(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}