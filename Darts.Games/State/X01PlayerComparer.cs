using Darts.Games.Models;
using System.Diagnostics.CodeAnalysis;

namespace Darts.Games.State;

public class X01PlayerComparer : IEqualityComparer<X01Player>
{
    public bool Equals(X01Player? x, X01Player? y)
    {
        return x?.IsPlayerActive == y?.IsPlayerActive
            && x?.Score == y?.Score
            && x?.PlayerOrder == y?.PlayerOrder
            && x?.HasOvershot == y?.HasOvershot
            && x?.IsInGame == y?.IsInGame;
    }

    public int GetHashCode([DisallowNull] X01Player obj)
    {
        return obj.GetHashCode();
    }
}
