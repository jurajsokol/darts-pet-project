using Darts.Avalonia.Enums;
using Darts.Avalonia.Models;
using Darts.Avalonia.ViewRouting;
using Darts.Games.Games;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System;
using System.Linq;

namespace Darts.Avalonia.ViewModels;

public partial class X01SetupViewModel : ReactiveObject
{
    private readonly IPageNavigation pageNavigation;

    public DartNumberModifierModel[] DartsNumberModifier { get; } = Enum.GetValues(typeof(DartsNumberModifier))
        .Cast<DartsNumberModifier>()
        .Select(x => new DartNumberModifierModel(x))
        .ToArray();

    public X01GameTypeModel[] GamePoints { get; } = Enum.GetValues(typeof(X01GameTypes))
        .Cast<X01GameTypes>()
        .Select(x => new X01GameTypeModel(x))
        .ToArray();

    [Reactive]
    private X01GameTypeModel x01GameType;

    [Reactive]
    private DartNumberModifierModel gameIn;

    [Reactive]
    private DartNumberModifierModel gameOut;

    public X01SetupViewModel(IPageNavigation pageNavigation)
    {
        this.pageNavigation = pageNavigation;
        GameIn = DartsNumberModifier.First();
        GameOut = DartsNumberModifier.First();
        X01GameType = GamePoints.First();
    }

    [ReactiveCommand]
    private void GoNext()
    {
        pageNavigation.GoNext<DartGameX01ViewModel>();
    }

    [ReactiveCommand]
    private void GoBack()
    {
        pageNavigation.GoBack();
    }
}
