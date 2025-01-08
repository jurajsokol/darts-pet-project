using Darts.Games.Enums;
using Darts.Games.Models;
using Darts.Games.State;
using DynamicData;
using System.Reactive.Linq;

namespace Darts.Games.Games;

public class X01 : IDartGame, IDisposable
{
    private const int MAX_THROWS_PER_ROUND = 3;
    private readonly Store<Player> gameStore;

    public Player ActualPlayer => gameStore.Players.Items.First(x => x.IsPlayerActive);
    public IObservable<IChangeSet<Player, int>> Players { get; }
    public IObservable<IChangeSet<PlayerMove, int>> PlayerRoundScore { get; }
    public IObservable<bool> CanSetNextPlayer { get; }


    public X01(IList<Player> players, Store<Player> gameStore)
    {
        Players = gameStore.Players.Connect();

        PlayerRoundScore = gameStore.PlayerRoundScore.Connect();

        gameStore.ResetPlayerScore();
        CanSetNextPlayer = PlayerRoundScore.ToCollection().Select(x => x.All(i => i.TargetButton != TargetButtonNum.None));
        this.gameStore = gameStore;
    }

    public void PlayerMove(TargetButtonNum number, TargetButtonType type)
    {
        if (gameStore.MoveCount >= MAX_THROWS_PER_ROUND)
        {
            return;
        }

        gameStore.MakeSnapshot();
        PlayerMove move = gameStore.PlayerRoundScore.Items
            .First(x => x.OrderNum == gameStore. MoveCount) with { TargetButton = number, TargetButtonType = type };

        gameStore.UpdatePlayerScore(move);
        Player actualPlayer = ActualPlayer with { Score = ActualPlayer.Score - move.GetScore };
        gameStore.UpdatePlayers(actualPlayer);
    }

    public bool NextPlayer()
    {
        if (HasPlayerWon(ActualPlayer))
        {
            return true;
        }
     
        gameStore.MakeSnapshot();

        if (IsOverShot(ActualPlayer))
        {
            int playerScore = gameStore.PlayerRoundScore.Items
                .Where(x => x.TargetButton != TargetButtonNum.None)
                .Select(x => (int)x.TargetButton * (int)x.TargetButtonType).Sum();
            gameStore.UpdatePlayers(ActualPlayer with { Score = ActualPlayer.Score + playerScore });
        }

        Player actualPlayer = (gameStore.Players.Items.FirstOrDefault(x => x.PlayerOrder == ActualPlayer.PlayerOrder + 1) ?? gameStore.Players.Items.First())
            with { IsPlayerActive = true };
        gameStore.UpdatePlayers(ActualPlayer with { IsPlayerActive = false });
        gameStore.UpdatePlayers(actualPlayer);

        gameStore.ResetPlayerScore();
        gameStore.ResetMoveCount();

        return false;
    }

    private bool IsOverShot(Player player)
    {
        return player.Score < 0;
    }

    private bool HasPlayerWon(Player player)
    {
        return player.Score == 0;
    }

    public void Undo()
    {
        gameStore.Undo();
    }

    public void Dispose()
    {
        gameStore.Dispose();
    }

    public Player[] GetPlayerResults()
    {
        return gameStore.Players.Items
            .OrderBy(x => x.Score)
            .Select((p, c) => p with { PlayerOrder = c })
            .ToArray();
    }
}
