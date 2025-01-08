using Avalonia.Controls;
using Darts.Avalonia.ViewModels;

namespace Darts.Avalonia.Views;

public partial class GameWinnerView : UserControl
{
    public GameWinnerView(WinnerViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
        DataContext = ViewModel;
    }

    public WinnerViewModel ViewModel { get; }
}