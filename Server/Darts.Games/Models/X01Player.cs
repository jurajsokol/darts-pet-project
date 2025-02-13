namespace Darts.Games.Models;

public record X01Player(
    string PlayerName,
    int Score,
    int PlayerOrder,
    bool IsPlayerActive,
    bool HasOvershot,
    bool IsInGame)
    : Player(PlayerName, Score, PlayerOrder, IsPlayerActive);