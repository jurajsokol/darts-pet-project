using Avalonia.Interactivity;
using Darts.Avalonia.ViewModels;
using Darts.Avalonia.Views.Dialog;

namespace Darts.Avalonia;

public partial class ConfirmGameClose : DialogBase<ConfirmGameExitViewModel>
{
    public ConfirmGameClose(ConfirmGameExitViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }

    private void Button_Click(object? sender, RoutedEventArgs e)
    {
        Close(DialogResult.Cancel);
    }

    private void OkButton_Click(object? sender, RoutedEventArgs e)
    {
        Close(DialogResult.Ok);
    }
}