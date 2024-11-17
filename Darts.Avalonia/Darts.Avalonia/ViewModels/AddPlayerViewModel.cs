using CommunityToolkit.Mvvm.ComponentModel;

namespace Darts.Avalonia.ViewModels;

public partial class AddPlayerViewModel : ObservableObject
{
    [ObservableProperty]
    private string name = string.Empty;
}
