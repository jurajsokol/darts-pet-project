using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Runtime.CompilerServices;

namespace Darts.Avalonia.Models;

public partial class Player : ReactiveObject
{
    public int ID { get; init; }

    [Reactive]
    private string name = string.Empty;

    [Reactive]
    private int orderNumber;

    public override bool Equals(object? obj)
    {
        if (obj is Player player)
        {
            return player.ID == ID;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return ID.GetHashCode();
    }
}
