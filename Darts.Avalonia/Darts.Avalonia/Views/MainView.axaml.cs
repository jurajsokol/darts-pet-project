using Avalonia.Controls;

namespace Darts.Avalonia.Views;

public partial class MainView : UserControl
{
    public MainView(MainMenuView mainMenuView)
    {
        InitializeComponent();
        NavigationPanel.Content = mainMenuView;
    }
}