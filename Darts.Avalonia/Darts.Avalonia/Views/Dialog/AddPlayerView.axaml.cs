using Avalonia;
using Avalonia.Input;
using Avalonia.Interactivity;
using Darts.Avalonia.ViewModels;

namespace Darts.Avalonia.Views.Dialog;

public partial class AddPlayerView : DialogBase<AddPlayerViewModel>
{
    public AddPlayerView(AddPlayerViewModel viewModel)
        :base(viewModel)
    {
        InitializeComponent();
        PlayerNameTextBox.AttachedToVisualTree += (_, _) => PlayerNameTextBox.Focus();
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