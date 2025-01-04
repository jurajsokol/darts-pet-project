using Avalonia.Data.Converters;
using Darts.Games.Enums;
using Darts.Games.Models;

namespace Darts.Avalonia.Views;

public static class Converters
{
    public static FuncValueConverter<CricketTargetButtonState, string> CricketStateToImage { get; } =
        new FuncValueConverter<CricketTargetButtonState, string>(state =>
        state switch
        {
            CricketTargetButtonState.NoHit => string.Empty,
            CricketTargetButtonState.OneHit => "/Resources/Icons/cricketOneHit.svg",
            CricketTargetButtonState.TwoHits => "/Resources/Icons/cricketTwoHits.svg",
            CricketTargetButtonState.Open => "/Resources/Icons/cricketOpenPosition.svg",

            _ => string.Empty,
        });
}