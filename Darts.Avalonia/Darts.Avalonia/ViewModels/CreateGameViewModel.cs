using Darts.Avalonia.Models;
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
using System.Collections.Specialized;

namespace Darts.Avalonia.ViewModels;

public partial class CreateGameViewModel : ReactiveObject
{
    private readonly IUnitOfWork db;
    private readonly IDialogManager dialogManager;
    private IObservable<bool> canStartGame;

    [Reactive]
    private GameTypeModel selectedGameType;

    public ObservableCollection<GameTypeModel> GameTypes { get; } = new ObservableCollection<GameTypeModel>(
        Enum.GetValues(typeof(GameTypes))
        .Cast<GameTypes>()
        .Select(x => new GameTypeModel(x)));

    public ObservableCollection<Player> Players { get; } = new();
    public IPageNavigation PageNavigation { get; }

    public ObservableCollection<Player> SelectedPlayers { get; } = new();

    [Reactive]
    private bool isVisible = false;

    public CreateGameViewModel(IUnitOfWork db, IPageNavigation pageNavigation, IDialogManager dialogManager)
    {
        this.db = db;
        PageNavigation = pageNavigation;
        this.dialogManager = dialogManager;
       
        IObservable<bool> isAnyPlayerSelected = Observable
            .FromEventPattern<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
                h => SelectedPlayers.CollectionChanged += h,
                h => SelectedPlayers.CollectionChanged -= h)
            .Select(x =>
            {
                ObservableCollection<Player> selectedPlayers = (x.Sender as ObservableCollection<Player>)!;
                return selectedPlayers.Any();
            });
        
        canStartGame = this.WhenAnyValue(x => x.SelectedGameType)
            .Select(x => x is not null)
            .CombineLatest(isAnyPlayerSelected, (gameType, player) => gameType && player);
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

    [ReactiveCommand(CanExecute = nameof(canStartGame))]
    private void StartGame()
    {
        PageNavigation.GoNext<DartGameX01ViewModel>();
    }
}
