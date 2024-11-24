using Darts.Games.Models;
using System.Diagnostics.CodeAnalysis;

namespace Darts.Games.State;

internal class PlayerComparer : IEqualityComparer<Player>
{
    public bool Equals(Player? x, Player? y)
    {
        return x?.IsPlayerActive == y?.IsPlayerActive
            && x?.Score == y?.Score
            && x?.PlayerOrder == y?.PlayerOrder;
    }

    public int GetHashCode([DisallowNull] Player obj)
    {
        return obj.GetHashCode();
    }
}
