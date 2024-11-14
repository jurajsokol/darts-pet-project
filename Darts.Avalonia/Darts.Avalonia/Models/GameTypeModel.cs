using Darts.Games.Games;

namespace Darts.Avalonia.Models;

public class GameTypeModel
{
    public string GameTypeName => GameType switch
    {
        GameTypes._301 => "301",
        GameTypes._401 => "401",
        GameTypes._501 => "501",
        GameTypes._601 => "601",
        _ => string.Empty,
    };

    public GameTypes GameType { get; }

    public GameTypeModel(GameTypes gameTypes)
    {
        GameType = gameTypes;
    }
}
