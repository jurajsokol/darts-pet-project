using System.Collections.Generic;

namespace Darts.DartGames
{
    internal static class GameFactory
    {
        public static X01 Game301(IList<Player> players)
        { 
            foreach (var player in players)
            {
                player.ActualScore = 301;
            }

            return new X01(players);
        }

        public static X01 Game501(IList<Player> players)
        {
            foreach (var player in players)
            {
                player.ActualScore = 501;
            }

            return new X01(players);
        }
    }
}
