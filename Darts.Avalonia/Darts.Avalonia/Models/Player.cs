using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace Darts.Avalonia.Models;

public partial class Player : ReactiveObject
{
    public int ID { get; init; }

    [Reactive]
    private string name = string.Empty;

    [Reactive]
    private int orderNumber;
}
