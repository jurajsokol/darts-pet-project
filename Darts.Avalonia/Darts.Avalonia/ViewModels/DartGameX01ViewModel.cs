using Darts.Avalonia.Factories;
using Darts.Avalonia.ViewRouting;
using Darts.Avalonia.Views;
using Darts.Avalonia.Views.Dialog;
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
using System.Threading.Tasks;

namespace Darts.Avalonia.ViewModels;

public partial class DartGameX01ViewModel : ReactiveObject, IActivatableViewModel
{
    private readonly IDartGame dartGame;
    private readonly GameScope gameScope;
    private readonly IAbstractFactory<IDialogScope<ConfirmGameExitViewModel>> dialogFactory;

    public ObservableCollection<Darts.Games.Models.Player> Players => players;
    private ObservableCollectionExtended<Darts.Games.Models.Player> players = new();

    public ObservableCollection<Games.Models.PlayerMove> PlayerRound => playerRound;

    public ViewModelActivator Activator { get; } = new ViewModelActivator();

    private ObservableCollectionExtended<Games.Models.PlayerMove> playerRound = new();

    public DartGameX01ViewModel(IDartGame dartGame, IScheduler guiScheduler, GameScope gameScope, IAbstractFactory<IDialogScope<ConfirmGameExitViewModel>> dialogFactory)
    {
        this.dartGame = dartGame;
        this.gameScope = gameScope;
        this.dialogFactory = dialogFactory;
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
        bool hasWon = dartGame.PlayerMove(score.DartNumbers.ToGameType(), score.Modifier.ToGameType());
        if (hasWon)
        {

        }
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

    public async Task CancelGame()
    {
        using IDialogScope<ConfirmGameExitViewModel> dialog = dialogFactory.Create();
        var result = await dialog.ShowDialog();

        if (result == DialogResult.Ok)
        { 
            gameScope.Dispose();
        }
    }
}
