using Darts.Games;
using Darts.Games.Models;
using DynamicData;

namespace Darts.Games.Games
{
    public interface IDartGame
    {
        IObservable<IChangeSet<Player, int>> Players { get; }
        IObservable<IChangeSet<PlayerMove, int>> PLayerRoundScore { get; }

        void PlayerMove(TargetButtonNum number, TargetButtonType type);
    }
}
