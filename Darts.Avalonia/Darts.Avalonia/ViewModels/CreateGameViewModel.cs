﻿using Darts.Avalonia.Models;
using Darts.Games.Games;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Darts.DAL;
using Darts.Avalonia.ViewRouting;
using Darts.Avalonia.Views.Dialog;
using System.Reactive.Linq;

namespace Darts.Avalonia.ViewModels;

public partial class CreateGameViewModel : ReactiveObject
{
    private readonly IUnitOfWork db;
    private readonly IDialogManager dialogManager;
    [Reactive]
    private GameTypeModel selectedGameType;

    public ObservableCollection<GameTypeModel> GameTypes { get; } = new ObservableCollection<GameTypeModel>(
        Enum.GetValues(typeof(GameTypes))
        .Cast<GameTypes>()
        .Select(x => new GameTypeModel(x)));

    public ObservableCollection<Player> Players { get; } = new();
    public IPageNavigation PageNavigation { get; }

    [Reactive]
    private List<Player> selectedPlayers = new();

    [Reactive]
    private bool isVisible = false;

    public CreateGameViewModel(IUnitOfWork db, IPageNavigation pageNavigation, IDialogManager dialogManager)
    {
        this.db = db;
        PageNavigation = pageNavigation;
        this.dialogManager = dialogManager;
    }

    public async Task LoadPlayers()
    {
        Players.Clear();
        IEnumerable<Player> players = (await db.Players.GetAll()).Select(x => x.ToModel());
        foreach (Player player in players)
        {
            Players.Add(player);
        }
    }

    [ReactiveCommand]
    private void SetVisibility()
    {
        IsVisible = !IsVisible;
    }

    [ReactiveCommand]
    private async Task AddPlayer()
    {
        (DialogResult, AddPlayerViewModel) data = await dialogManager.ShowDialog<AddPlayerViewModel>();
        if (data.Item1 == DialogResult.Ok)
        { 
            await db.Players.Add(new DAL.Entities.Player() { Name = data.Item2.Name });
            await db.CompleteAsync();
            await LoadPlayers();
        }
    }

    [ReactiveCommand]
    private async Task RemoveSelectedPlayers()
    {
        foreach (int id in SelectedPlayers.Select(x => x.ID).ToArray())
        {
            await db.Players.Delete(id);
        }

        await db.CompleteAsync();
        await LoadPlayers();
    }

    [ReactiveCommand]
    private void StartGame()
    {
        PageNavigation.GoNext<DartGameX01ViewModel>();
    }
}
