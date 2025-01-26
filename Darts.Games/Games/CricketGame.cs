using Darts.Games.Enums;
using Darts.Games.Models;
using Darts.Games.State;

namespace Darts.Games.Games;

public class CricketGame : CricketGameBase
{
    public CricketGame(Store<CricketPlayer> gameStore) : base(gameStore)
    { }

    public override CricketPlayer[] GetPlayersResults()
    {
        return gameStore.Players.Items
            .OrderByDescending(x => x.Score)
            .Select((p, c) => p with { PlayerOrder = c })
            .ToArray();
    }

    protected override bool HasPlayerWon()
    {
        return gameStore.Players.Items
            .OrderByDescending(x => x.Score)
            .First().CricketDartButtonStates
            .All(x => x.CricketTargetButtonState == CricketTargetButtonState.Closed || x.CricketTargetButtonState == CricketTargetButtonState.Open);
    }

    protected override CricketPlayer UpdatePlayersScore(CricketPlayer actualPlayer, int score, TargetButtonNum buttonNum)
    {
        CricketPlayer updatedPlayer = actualPlayer with { Score = actualPlayer.Score + score };
        gameStore.UpdatePlayers(updatedPlayer);
        return updatedPlayer;
    }
}
