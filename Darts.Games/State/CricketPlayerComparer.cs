using Darts.Games.Models;
using System.Diagnostics.CodeAnalysis;

namespace Darts.Games.State;

public class CricketPlayerComparer : IEqualityComparer<CricketPlayer>
{
    public bool Equals(CricketPlayer? x, CricketPlayer? y)
    {
        if ((x is null) || (y is null))
        { 
            return false;
        }

        for (int i = 0; i < x.CricketDartButtonStates.Length; i++)
        {
            if (x.CricketDartButtonStates[i] == y.CricketDartButtonStates[i])
            {
                continue;
            }

            return false;
        }

        return x?.IsPlayerActive == y?.IsPlayerActive
            && x?.Score == y?.Score
            && x?.PlayerOrder == y?.PlayerOrder ;
    }

    public int GetHashCode([DisallowNull] CricketPlayer obj)
    {
        return obj.GetHashCode();
    }
}
