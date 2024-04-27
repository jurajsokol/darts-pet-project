using Darts.Games.Models;

namespace Darts.Games.Games
{
    public interface IDartGameFactory
    {
        IDartGame GetGame(GameTypes gameType, IList<Player> players);
    }
}
