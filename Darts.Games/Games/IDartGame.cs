using Darts.Games.Models;
using DynamicData;

namespace Darts.Games.Games;

public interface IDartGame : IDisposable
{
    IObservable<IChangeSet<Player, int>> Players { get; }
    IObservable<IChangeSet<PlayerMove, int>> PlayerRoundScore { get; }
    IObservable<bool> CanSetNextPlayer { get; }

    bool PlayerMove(TargetButtonNum number, TargetButtonType type);
    void NextPlayer();
    void Undo();
}
