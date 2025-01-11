using Darts.Games.Enums;
using Darts.Games.Models;
using Darts.Games.State;
using DynamicData;
using System.Reactive.Linq;

namespace Darts.Games.Games;

public class X01 : IDartGame<X01Player>, IDisposable
{
    private const int MAX_THROWS_PER_ROUND = 3;
    private readonly TargetButtonType gameIn;
    private readonly TargetButtonType gameOut;
    private readonly Store<X01Player> gameStore;

    public X01Player ActualPlayer => gameStore.Players.Items.First(x => x.IsPlayerActive);
    public IObservable<IChangeSet<X01Player, int>> Players { get; }
    public IObservable<IChangeSet<PlayerMove, int>> PlayerRoundScore { get; }
    public IObservable<bool> CanSetNextPlayer { get; }


    public X01(TargetButtonType gameIn, TargetButtonType gameOut, Store<X01Player> gameStore)
    {
        Players = gameStore.Players.Connect();

        PlayerRoundScore = gameStore.PlayerRoundScore.Connect();

        gameStore.ResetPlayerScore();
        CanSetNextPlayer = PlayerRoundScore.ToCollection().Select(x => x.All(i => i.TargetButton != TargetButtonNum.None));
        this.gameIn = gameIn;
        this.gameOut = gameOut;
        this.gameStore = gameStore;
    }

    public bool PlayerMove(TargetButtonNum number, TargetButtonType type)
    {
        if (gameStore.MoveCount >= MAX_THROWS_PER_ROUND)
        {
            return false;
        }

        gameStore.MakeSnapshot();
        PlayerMove move = gameStore.PlayerRoundScore.Items
            .First(x => x.OrderNum == gameStore. MoveCount) with { TargetButton = number, TargetButtonType = type };

        gameStore.UpdatePlayerScore(move);

        X01Player player = ActualPlayer;

        if (player.IsInGame)
        {
            X01Player actualPlayer = player with { Score = player.Score - move.GetScore };
            actualPlayer = actualPlayer with { HasOvershot = IsOverShot(actualPlayer, move) };
            gameStore.UpdatePlayers(actualPlayer);
        }
        else
        {
            X01Player playerUpdate = gameIn switch
            {
                TargetButtonType.Double => DoubleIn(player, move),
                TargetButtonType.Triple => MasterIn(player, move),

                _ => throw new InvalidOperationException("X01 game in exception")
            };

            gameStore.UpdatePlayers(playerUpdate);
        }

        if (HasPlayerWon(ActualPlayer, move))
        {
            return true;
        }

        return false;
    }

    private X01Player DoubleIn(X01Player player, PlayerMove move)
    {
        if (move.TargetButtonType == TargetButtonType.Double)
        {
            return player with { IsInGame = true, Score = player.Score - move.GetScore };
        }

        return player;
    }

    private X01Player MasterIn(X01Player player, PlayerMove move)
    {
        if (move.TargetButtonType == TargetButtonType.Double || move.TargetButtonType == TargetButtonType.Triple)
        {
            return player with { IsInGame = true, Score = player.Score - move.GetScore };
        }

        return player;
    }

    public void NextPlayer()
    {
        X01Player actualPlayer = ActualPlayer;
     
        gameStore.MakeSnapshot();

        if (actualPlayer.HasOvershot)
        {
            int playerScore = gameStore.PlayerRoundScore.Items
                .Where(x => x.TargetButton != TargetButtonNum.None)
                .Select(x => (int)x.TargetButton * (int)x.TargetButtonType).Sum();

            actualPlayer = actualPlayer with { Score = actualPlayer.Score + playerScore, HasOvershot = false };
        }

        X01Player nextPlayer = (gameStore.Players.Items.FirstOrDefault(x => x.PlayerOrder == actualPlayer.PlayerOrder + 1) ?? gameStore.Players.Items.First())
            with { IsPlayerActive = true };
        gameStore.UpdatePlayers(actualPlayer with { IsPlayerActive = false }, nextPlayer);

        gameStore.ResetPlayerScore();
        gameStore.ResetMoveCount();
    }

    private bool IsOverShot(Player player, PlayerMove move)
    {
        return gameOut switch
        {
            TargetButtonType.Double => player.Score == 1 || player.Score < 0,
            TargetButtonType.Triple => player.Score == 1 || player.Score < 0,
            _ => player.Score < 0,
        };
    }

    private bool HasPlayerWon(Player player, PlayerMove move)
    {
        return gameOut switch
        {
            TargetButtonType.Double => player.Score == 0 && move.TargetButtonType == TargetButtonType.Double,
            TargetButtonType.Triple => player.Score == 0 && move.TargetButtonType == TargetButtonType.Double && move.TargetButtonType == TargetButtonType.Triple,
            _ => player.Score < 0,
        };
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
