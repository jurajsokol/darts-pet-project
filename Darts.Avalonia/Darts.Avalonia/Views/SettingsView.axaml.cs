using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Styling;
using System.Reflection;
using System.Linq;

namespace Darts.Avalonia;

public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        InitializeComponent();
        AssemblyMetadataAttribute[] assemblyAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes<AssemblyMetadataAttribute>().ToArray();

        RevisionText.Text = assemblyAttributes.FirstOrDefault(x => x.Key == "RevisionCount")?.Value;
        BuildDateText.Text = assemblyAttributes.FirstOrDefault(x => x.Key == "BuildDate")?.Value;
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