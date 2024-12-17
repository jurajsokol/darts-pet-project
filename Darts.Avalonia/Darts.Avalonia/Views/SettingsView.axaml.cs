using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Styling;

namespace Darts.Avalonia;

public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        InitializeComponent();
    }

    private void ThemeButton_Click(object? sender, RoutedEventArgs e)
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