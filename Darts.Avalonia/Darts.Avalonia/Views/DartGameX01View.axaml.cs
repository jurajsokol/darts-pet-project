using Avalonia.Controls;
using Darts.Avalonia.ViewModels;

namespace Darts.Avalonia;

public partial class DartGameX01View : UserControl
{
    public DartGameX01View(DartGameX01ViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}