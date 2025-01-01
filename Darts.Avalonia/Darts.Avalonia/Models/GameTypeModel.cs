using Darts.Games.Enums;

namespace Darts.Avalonia.Models;

public class GameTypeModel
{
    public string GameTypeName => GameType switch
    {
        GameTypes.X01 => "X01",
        GameTypes.Cricket => "Cricket",
        GameTypes.CutThroat => "Cut throat",
        _ => string.Empty,
    };

    public GameTypes GameType { get; }

    public GameTypeModel(GameTypes gameTypes)
    {
        GameType = gameTypes;
    }
}
