using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Darts.Games.Games;
using Darts.Games.Models;
using Darts.MVVM.Enums;
using Darts.MVVM;
using DynamicData;
using DynamicData.Binding;
using System.Collections.ObjectModel;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Darts.MVVM;

namespace Darts.MVVM.ViewModels;

public partial class DartsGameViewModel : ObservableObject, IDisposable
{
    private CompositeDisposable disposables = new();
    private IDartGame game;
    private bool canSetNextPlayer = false;

    [ObservableProperty]
    private string actualThrowScore;


    private ObservableCollectionExtended<Darts.Games.Models.Player> players = new();
    public ObservableCollection<Darts.Games.Models.Player> Players => players;

    public ObservableCollection<Games.Models.PlayerMove> PlayerRound => playerRound;
    private ObservableCollectionExtended<Games.Models.PlayerMove> playerRound = new();

    public DartsGameViewModel(INewDartGameArgs newGameArgs, IDartGameFactory gameFactory, CurrentThreadScheduler guiScheduler)
    {
        game = gameFactory.GetGame(newGameArgs.GameType, newGameArgs.GamePlayers.Select((x, i) => x.ToDartPlayer(i)).ToArray())
            .DisposeWith(disposables);
        game.Players
            .Sort(SortExpressionComparer<Games.Models.Player>.Ascending(p => p.PlayerOrder))
            .ObserveOn(guiScheduler)
            .Bind(players)
            .Subscribe()
            .DisposeWith(disposables);

        game.PlayerRoundScore
            .Sort(SortExpressionComparer<PlayerMove>.Ascending(p => p.OrderNum))
            .ObserveOn(guiScheduler)
            .Bind(playerRound)
            .Subscribe()
            .DisposeWith(disposables);

        game.CanSetNextPlayer
            .ObserveOn(guiScheduler)
            .Subscribe(value =>
            {
                canSetNextPlayer = value;
                NextPlayerCommand.NotifyCanExecuteChanged();
            })
            .DisposeWith(disposables);
    }

    [RelayCommand]
    private void DartClick((DartNumbers number, DartsNumberType type) args)
    {
        game.PlayerMove(args.number.ToGameType(), args.type.ToGameType());
    }

    [RelayCommand(CanExecute = nameof(CanSetNextPlayer))]
    private void NextPlayer()
    {
        game.NextPlayer();
    }

    private bool CanSetNextPlayer() => canSetNextPlayer;

    public void Dispose()
    {
        disposables.Dispose();
    }
}
