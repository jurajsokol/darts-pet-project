using Darts.Games.Models;

namespace Darts.Games.Games
{
    public class DartGameFactory : IDartGameFactory
    {
        public IDartGame GetGame(GameTypes gameType, IList<Player> players)
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
    }
}
