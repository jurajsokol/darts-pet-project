namespace Darts.Games.Models;

public record Player(string PlayerName, int Score, int PlayerOrder, bool IsPlayerActive)
{
    public bool HasWon => Score == 0;
}
