using Avalonia.ReactiveUI;
using Darts.Avalonia.ViewModels;
using ReactiveUI;

namespace Darts.Avalonia.Views.X01GameView;

public partial class X01KeyBoardView : ReactiveUserControl<DartGameX01ViewModel>, IActivatableView
{
    public X01KeyBoardView()
    {
        InitializeComponent();
    }
}