using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using Darts.Avalonia.ViewModels;

namespace Darts.Avalonia;

public partial class SettingsView : ReactiveUserControl<SettingsViewModel>
{
    public SettingsView(SettingsViewModel viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();
    }

    private void ThemeButton_Click(object? sender, RoutedEventArgs e)
    {
       
    }
}