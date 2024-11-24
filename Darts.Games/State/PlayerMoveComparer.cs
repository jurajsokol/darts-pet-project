using Darts.Games.Models;
using System.Diagnostics.CodeAnalysis;

namespace Darts.Games.State;

internal class PlayerMoveComparer : IEqualityComparer<PlayerMove>
{
    public bool Equals(PlayerMove? x, PlayerMove? y)
    {
        return x?.TargetButtonType == y?.TargetButtonType
            && x?.OrderNum == y?.OrderNum
            && x?.TargetButton == y?.TargetButton;
    }

    public int GetHashCode([DisallowNull] PlayerMove obj)
    {
        return obj.GetHashCode();
    }
}
