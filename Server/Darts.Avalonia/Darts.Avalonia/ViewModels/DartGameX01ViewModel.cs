﻿using Darts.Avalonia.Factories;
using Darts.Avalonia.GameScope;
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
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Darts.Avalonia.ViewModels;

public partial class DartGameX01ViewModel : KeyboardViewModel, IActivatableViewModel
{
    private readonly X01 dartGame;
    private readonly X01GameScope gameScope;
    private readonly IAbstractFactory<IDialogScope<ConfirmGameExitViewModel>> dialogFactory;

    public ObservableCollection<Darts.Games.Models.X01Player> Players => players;
    private ObservableCollectionExtended<Darts.Games.Models.X01Player> players = new();

    public ObservableCollection<Games.Models.PlayerMove> PlayerRound => playerRound;

    private ObservableCollectionExtended<Games.Models.PlayerMove> playerRound = new();

    public DartGameX01ViewModel(X01 dartGame, IScheduler guiScheduler, X01GameScope gameScope, IAbstractFactory<IDialogScope<ConfirmGameExitViewModel>> dialogFactory)
    {
        this.dartGame = dartGame;
        this.gameScope = gameScope;
        this.dialogFactory = dialogFactory;
        this.WhenActivated(disposable =>
        {
            dartGame.Players
                .ObserveOn(guiScheduler)

                .SortAndBind(players, SortExpressionComparer<Games.Models.Player>.Ascending(p => p.PlayerOrder))
                .Subscribe()
                .DisposeWith(disposable);

            dartGame.PlayerRoundScore
               .ObserveOn(guiScheduler)
               .SortAndBind(playerRound, SortExpressionComparer<PlayerMove>.Ascending(p => p.OrderNum))
               .Subscribe()
               .DisposeWith(disposable);

            Disposable.Create(() =>
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
            gameScope.ExitGame();
        }
    }

    internal override void OnDartScore(DartScore score)
    {
        if (dartGame.PlayerMove(score.DartNumbers.ToGameType(), score.Modifier.ToGameType()))
        {
            gameScope.ShowWinnersView(dartGame.GetPlayerResults().Select(x => x.ToModel()).ToArray());
        }
    }
}
