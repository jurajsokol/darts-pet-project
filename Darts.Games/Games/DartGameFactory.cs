using Darts.Games.Models;
using System.Collections.ObjectModel;

namespace Darts.Games.Games
{
    public class DartGameFactory : IDartGameFactoryGetter, IDartGameFactorySetter
    {
        private GameTypes gameType;
        private IReadOnlyList<Player> players;

        public IDartGame GetGame()
        {
            return gameType switch
            {
                GameTypes._301 => new X01(players, 301),
                GameTypes._401 => new X01(players, 401),
                GameTypes._501 => new X01(players, 501),
                GameTypes._601 => new X01(players, 601),

                _ => throw new NotImplementedException($"Game type {gameType} is not implemented yet"),
            };
        }

        public void SetGameAndPlayers(GameTypes gameType, IList<Player> players)
        {
            this.gameType = gameType;
            this.players = new ReadOnlyCollection<Player>(players);
        }
    }
}
