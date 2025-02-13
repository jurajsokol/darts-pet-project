using System.Collections.Immutable;

namespace Darts.Games.Models;

public record CricketPlayer(
    string PlayerName,
    int Score,
    int PlayerOrder,
    bool IsPlayerActive,
    ImmutableArray<CricketDartButtonState> CricketDartButtonStates)
    : Player(PlayerName, Score, PlayerOrder, IsPlayerActive);