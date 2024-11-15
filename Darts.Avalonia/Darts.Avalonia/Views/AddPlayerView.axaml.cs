using Avalonia.ReactiveUI;
using Darts.Avalonia.ViewModels;

namespace Darts.Avalonia;

public partial class AddPlayerView : ReactiveUserControl<AddPlayerViewModel>
{
    public AddPlayerView()
    {
        InitializeComponent();
    }
}