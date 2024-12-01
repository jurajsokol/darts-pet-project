using Darts.Avalonia.Enums;

namespace Darts.Avalonia.Models;

public class DartNumberModifierModel
{
    public string ModifierTypeName => DartsNumberModifier switch
    {
        DartsNumberModifier.Double => "Double",
        DartsNumberModifier.Single => "Single",
        DartsNumberModifier.Triple => "Triple",

        _ => string.Empty,
    };

    public DartsNumberModifier DartsNumberModifier { get; }

    public DartNumberModifierModel(DartsNumberModifier dartsNumberModifier)
    {
        DartsNumberModifier = dartsNumberModifier;
    }
}
