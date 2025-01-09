using Darts.Games.Enums;
using Darts.Games.Models;
using Darts.Games.State;
using DynamicData;
using System.Collections.Immutable;
using System.Reactive.Linq;

namespace Darts.Games.Games;

public class CricketGame
{
    private const int MAX_THROWS_PER_ROUND = 3;
    private readonly Store<CricketPlayer> gameStore;

    public CricketPlayer ActualPlayer => gameStore.Players.Items.First(x => x.IsPlayerActive);
    public IObservable<IChangeSet<CricketPlayer, int>> Players { get; }
    public IObservable<IChangeSet<PlayerMove, int>> PlayerRoundScore { get; }
    public IObservable<bool> CanSetNextPlayer { get; }

    public CricketGame(IList<CricketPlayer> players, Store<CricketPlayer> gameStore)
    {
        Players = gameStore.Players.Connect();

        PlayerRoundScore = gameStore.PlayerRoundScore.Connect();

        gameStore.ResetPlayerScore();
        CanSetNextPlayer = PlayerRoundScore.ToCollection().Select(x => x.All(i => i.TargetButton != TargetButtonNum.None));
        this.gameStore = gameStore;
    }

    public void PlayerMove(TargetButtonNum number, TargetButtonType type)
    {
        if (((int)number) < 15)
        {
            return;
        }

        CricketPlayer actualPlayer = ActualPlayer;

        if (gameStore.MoveCount >= MAX_THROWS_PER_ROUND)
        {
            return;
        }

        gameStore.MakeSnapshot();
        PlayerMove move = gameStore.PlayerRoundScore.Items
            .First(x => x.OrderNum == gameStore.MoveCount) with { TargetButton = number, TargetButtonType = type };

        gameStore.UpdatePlayerScore(move);

        CricketDartButtonState buttonState = actualPlayer.CricketDartButtonStates.First(x => x.TargetButtonNum == number);

        // closed position
        if (buttonState.CricketTargetButtonState == CricketTargetButtonState.Closed)
        {
            return;
        }

        // opened position
        if (buttonState.CricketTargetButtonState == CricketTargetButtonState.Open)
        {
            gameStore.UpdatePlayers(actualPlayer with { Score = actualPlayer.Score + move.GetScore });
        }
        // not yet opened position
        else
        {
            int hitCount = (int)buttonState.CricketTargetButtonState + (int)type;
            int newPlayerScore = actualPlayer.Score;
            CricketDartButtonState newButtonState = buttonState;
            if (hitCount > (int)CricketTargetButtonState.Open)
            {
                bool isLastPlayer = gameStore.Players.Items
                    .Count(x => x.CricketDartButtonStates
                        .First(y => y.TargetButtonNum == number)
                        .CricketTargetButtonState != CricketTargetButtonState.Open) == 1;
                // last player closes position
                if (isLastPlayer)
                {
                    gameStore.UpdatePlayers(gameStore.Players.Items
                        .Select(x =>
                        {
                            CricketDartButtonState numberToClose = x.CricketDartButtonStates.First(y => y.TargetButtonNum == number);
                            ImmutableArray<CricketDartButtonState> states = x.CricketDartButtonStates
                                .Replace(
                                    numberToClose,
                                    numberToClose with { CricketTargetButtonState = CricketTargetButtonState.Closed });

                            return x with { CricketDartButtonStates = states };
                        })
                        .ToArray());

                    return;
                }
                else
                { 
                    int playerPointsMultiplier = hitCount - (int)CricketTargetButtonState.Open;
                    newButtonState = buttonState with { CricketTargetButtonState = CricketTargetButtonState.Open };
                    newPlayerScore = actualPlayer.Score + (playerPointsMultiplier * (int)number);
                }
            }
            else
            {
                newButtonState = buttonState with { CricketTargetButtonState = (CricketTargetButtonState)((int)buttonState.CricketTargetButtonState + (int)type) };
            }

            ImmutableArray<CricketDartButtonState> states = actualPlayer.CricketDartButtonStates.Replace(buttonState, newButtonState);
            gameStore.UpdatePlayers(actualPlayer with { Score = newPlayerScore, CricketDartButtonStates = states});
        }

        // winner
        return;
    }

    public bool NextPlayer()
    {
        if (HasPlayerWon())
        {
            return true;
        }

        gameStore.MakeSnapshot();

        CricketPlayer actualPlayer = (gameStore.Players.Items.FirstOrDefault(x => x.PlayerOrder == ActualPlayer.PlayerOrder + 1) ?? gameStore.Players.Items.First())
            with { IsPlayerActive = true };
        gameStore.UpdatePlayers(ActualPlayer with { IsPlayerActive = false });
        gameStore.UpdatePlayers(actualPlayer);

        gameStore.ResetPlayerScore();
        gameStore.ResetMoveCount();
        return false;
    }

    public void Undo()
    {
        gameStore.Undo();
    }

    private bool HasPlayerWon()
    {
        return gameStore.Players.Items
            .OrderByDescending(x => x.Score)
            .First().CricketDartButtonStates
            .All(x => x.CricketTargetButtonState == CricketTargetButtonState.Closed || x.CricketTargetButtonState == CricketTargetButtonState.Open);
    }

    public CricketPlayer[] GetPlayersResults()
    {
        return gameStore.Players.Items
            .OrderByDescending(x => x.Score)
            .Select((p, c) => p with { PlayerOrder = c })
            .ToArray();
    }
}
