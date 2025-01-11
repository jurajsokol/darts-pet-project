using Darts.Games.Enums;
using Darts.Games.Models;
using Darts.Games.State;

namespace Darts.Games.Games;

public class CutThroatGame : CricketGameBase
{
    public CutThroatGame(Store<CricketPlayer> gameStore) : base(gameStore)
    { }

    public override CricketPlayer[] GetPlayersResults()
    {
        return gameStore.Players.Items
            .OrderBy(x => x.Score)
            .Select((p, c) => p with { PlayerOrder = c })
            .ToArray();
    }

    protected override bool HasPlayerWon()
    {
        return gameStore.Players.Items
            .OrderBy(x => x.Score)
            .First().CricketDartButtonStates
            .All(x => x.CricketTargetButtonState == CricketTargetButtonState.Closed || x.CricketTargetButtonState == CricketTargetButtonState.Open);
    }

    protected override void UpdatePlayersScore(CricketPlayer actualPlayer, int score, TargetButtonNum buttonNum)
    {
        IEnumerable<CricketPlayer> players = gameStore.Players.Items.Where(x =>
            {
                CricketDartButtonState buttonNumState = x.CricketDartButtonStates.First(d => d.TargetButtonNum == buttonNum);
                return buttonNumState.CricketTargetButtonState != CricketTargetButtonState.Closed && buttonNumState.CricketTargetButtonState != CricketTargetButtonState.Open;
            })
            .Select(x => x with { Score = x.Score + score } );

        gameStore.UpdatePlayers(players.ToArray());
    }
}
