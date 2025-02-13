using Darts.Avalonia.Factories;
using Darts.Avalonia.Views.Dialog;
using Darts.Games.Games;
using DynamicData.Binding;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive.Concurrency;
using ReactiveUI.SourceGenerators;
using System.Threading.Tasks;
using DynamicData;
using System.Reactive.Linq;
using System;
using System.Reactive.Disposables;
using Darts.Avalonia.GameScope;
using Darts.Avalonia.Views;
using Darts.Games.Models;
using System.Linq;

namespace Darts.Avalonia.ViewModels;

public partial class CricketGameViewModel : KeyboardViewModel, IActivatableViewModel
{
    private readonly IGameScope gameScope;
    private readonly IAbstractFactory<IDialogScope<ConfirmGameExitViewModel>> dialogFactory;
    private readonly CricketGameBase game;

    public ObservableCollection<Darts.Games.Models.CricketPlayer> Players => players;

    private ObservableCollectionExtended<Darts.Games.Models.CricketPlayer> players = new();

    public ObservableCollection<Games.Models.PlayerMove> PlayerRound => playerRound;
    private ObservableCollectionExtended<Games.Models.PlayerMove> playerRound = new();

    public CricketGameViewModel(CricketGameBase game, IScheduler guiScheduler, IGameScope gameScope, IAbstractFactory<IDialogScope<ConfirmGameExitViewModel>> dialogFactory)
    {
        this.game = game;
        this.gameScope = gameScope;
        this.dialogFactory = dialogFactory;

        this.WhenActivated(disposable =>
        {
            game.Players
                .ObserveOn(guiScheduler)
                .SortAndBind(players, SortExpressionComparer<Games.Models.CricketPlayer>.Ascending(p => p.PlayerOrder))
                .Subscribe()
                .DisposeWith(disposable);

            game.PlayerRoundScore
               .ObserveOn(guiScheduler)
               .SortAndBind(playerRound, SortExpressionComparer<PlayerMove>.Ascending(p => p.OrderNum))
               .Subscribe()
               .DisposeWith(disposable);

            Disposable
                .Create(() =>
                {
                    players.Clear();
                    playerRound.Clear();
                })
                .DisposeWith(disposable);
        });
    }

    [ReactiveCommand]
    private void NextPlayer()
    {
        game.NextPlayer();
    }

    [ReactiveCommand]
    private void Undo()
    {
        game.Undo();
    }

    internal override void OnDartScore(DartScore score)
    {
        if (game.PlayerMove(score.DartNumbers.ToGameType(), score.Modifier.ToGameType()))
        {
            gameScope.ShowWinnersView(game.GetPlayersResults().Select(x => x.ToModel()).ToArray());
        }
    }

    public async Task CancelGame()
    {
        using IDialogScope<ConfirmGameExitViewModel> dialog = dialogFactory.Create();
        var result = await dialog.ShowDialog();

        if (result == DialogResult.Ok)
        {
            gameScope.ExitGame();
        }
    }
}
