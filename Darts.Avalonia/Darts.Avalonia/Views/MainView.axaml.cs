using Avalonia.Controls;
using Darts.Avalonia.ViewRouting;

namespace Darts.Avalonia.Views;

public partial class MainView : UserControl
{
    public MainView(PageNavigation pageNavigation)
    {
        InitializeComponent();
        RouterViewHost.Router = pageNavigation.RoutingState;
        pageNavigation.GoNextCommand.Execute();
    }
}