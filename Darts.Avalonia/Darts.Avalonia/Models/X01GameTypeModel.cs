using Darts.Games.Enums;

namespace Darts.Avalonia.Models;

public class X01GameTypeModel
{
    public string GameTypeName => GameType switch
    {
        X01GameTypes._301 => "301",
        X01GameTypes._401 => "401",
        X01GameTypes._501 => "501",
        X01GameTypes._601 => "601",
        _ => string.Empty,
    };

    public X01GameTypes GameType { get; }

    public X01GameTypeModel(X01GameTypes gameTypes)
    {
        GameType = gameTypes;
    }
}
