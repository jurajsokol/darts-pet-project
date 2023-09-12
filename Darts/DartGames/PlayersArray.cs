using System.Collections.Generic;

namespace Darts.DartGames
{
    internal static class PLayersArrayExtensions
    {
        public static Player GetActualPlayer(this IList<Player> playersList)
        { 
            IEnumerator<Player> enumerator = playersList.GetEnumerator();
            return enumerator.Current;
        }
        public static Player GetNextPlayer(this IList<Player> playersList)
        {
            playersList.SetNextPlayer();
            return playersList.GetActualPlayer();
        }

        public static void SetNextPlayer(this IList<Player> playersList)
        {
            IEnumerator<Player> enumerator = playersList.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                enumerator.Reset();
            }
        }
    }
}
