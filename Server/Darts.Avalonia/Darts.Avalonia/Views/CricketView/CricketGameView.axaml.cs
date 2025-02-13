using Avalonia.ReactiveUI;
using Darts.Avalonia.ViewModels;

namespace Darts.Avalonia;

public partial class CricketGameView : ReactiveUserControl<CricketGameViewModel>
{
    public CricketGameView(CricketGameViewModel viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();
    }
}