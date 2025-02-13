using Avalonia.Controls;
using Darts.Avalonia.ViewModels;

namespace Darts.Avalonia.Views.X01GameView;

public partial class X01GameSetup : UserControl
{
    public X01GameSetup(X01SetupViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}