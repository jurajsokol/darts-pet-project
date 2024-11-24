using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Styling;
using Darts.Avalonia.ViewModels;

namespace Darts.Avalonia.Views;

public partial class CreateGameView : UserControl
{
    public CreateGameViewModel ViewModel { get; }

    public CreateGameView(CreateGameViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = ViewModel;
        InitializeComponent();
        Loaded += async (_, _) => await ViewModel.LoadPlayers();
    }

    private void Button_Click(object? sender, RoutedEventArgs e)
    {
        string actualThemeVariant = (string)App.Current.ActualThemeVariant.Key;
        if (actualThemeVariant == "Light")
        {
            App.Current.RequestedThemeVariant = new ThemeVariant("Dark", null);
        }
        else
        {
            App.Current.RequestedThemeVariant = new ThemeVariant("Light", null);
        }
    }
}