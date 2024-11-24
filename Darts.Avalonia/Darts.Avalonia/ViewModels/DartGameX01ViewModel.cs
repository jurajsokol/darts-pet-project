using Darts.Avalonia.ViewRouting;
using Darts.Avalonia.Views;
using Darts.Games.Games;
using Darts.Games.Models;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Darts.Avalonia.ViewModels;

public partial class DartGameX01ViewModel : ReactiveObject, IActivatableViewModel
{
    private readonly IPageNavigation pageNavigation;
    private readonly IDartGame dartGame;

    public ObservableCollection<Darts.Games.Models.Player> Players => players;
    private ObservableCollectionExtended<Darts.Games.Models.Player> players = new();

    public ObservableCollection<Games.Models.PlayerMove> PlayerRound => playerRound;

    public ViewModelActivator Activator { get; } = new ViewModelActivator();

    private ObservableCollectionExtended<Games.Models.PlayerMove> playerRound = new();

    public DartGameX01ViewModel(IPageNavigation pageNavigation, IDartGame dartGame, IScheduler guiScheduler)
    {
        this.pageNavigation = pageNavigation;
        this.dartGame = dartGame;

        this.WhenActivated(disposable =>
        {
            dartGame.Players
                .Sort(SortExpressionComparer<Games.Models.Player>.Ascending(p => p.PlayerOrder))
                .ObserveOn(guiScheduler)
                .Bind(players)
                .Subscribe()
                .DisposeWith(disposable);

            dartGame.PlayerRoundScore
               .Sort(SortExpressionComparer<PlayerMove>.Ascending(p => p.OrderNum))
               .ObserveOn(guiScheduler)
               .Bind(playerRound)
               .Subscribe()
               .DisposeWith(disposable);
        });
    }

    [ReactiveCommand]
    private void DartClick(DartScore score)
    {
        dartGame.PlayerMove(score.DartNumbers.ToGameType(), score.Modifier.ToGameType());
    }

    [ReactiveCommand]
    private void NextPlayer()
    {
        dartGame.NextPlayer();
    }

    [ReactiveCommand]
    private void Undo()
    {
        dartGame.Undo();
    }

    public void CancelGame()
    {
        pageNavigation.GoBack();
    }
}
