using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace Darts.Avalonia.ViewModels;

public partial class AddPlayerViewModel : ReactiveObject
{
    [Reactive]
    private string name = string.Empty;
}
