using Darts.Games.Models;

namespace Darts.Games.Games
{
    public interface IDartGameFactorySetter
    {
        void SetGameAndPlayers(GameTypes gameType, IList<Player> players);
    }
}
