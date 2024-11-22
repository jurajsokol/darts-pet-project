namespace Darts.Avalonia.Models;

public record GamePlayer(string PlayerName, int Score, int PlayerOrder, bool IsPlayerActive)
{
    public bool HasWon => Score == 0;
}
