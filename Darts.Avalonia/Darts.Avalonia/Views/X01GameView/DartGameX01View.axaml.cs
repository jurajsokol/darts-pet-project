using Avalonia.ReactiveUI;
using Darts.Avalonia.ViewModels;

namespace Darts.Avalonia.Views.X01GameView;

public partial class DartGameX01View : ReactiveUserControl<DartGameX01ViewModel>
{
    public DartGameX01View(DartGameX01ViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
        DataContext = viewModel;
    }
}