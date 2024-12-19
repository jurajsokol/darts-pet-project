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
using System.Reactive.Linq;
using System.Collections.Specialized;

namespace Darts.Avalonia.ViewModels;

public partial class CreateGameViewModel : ReactiveObject
{
    private readonly IUnitOfWork db;

    public IObservable<bool> CanStartGame { get; }

    [Reactive]
    private GameTypeModel selectedGameType;

    public GameTypeModel[] GameTypes { get; } = Enum.GetValues(typeof(GameTypes))
        .Cast<GameTypes>()
        .Select(x => new GameTypeModel(x))
        .ToArray();

    public ObservableCollection<Player> Players { get; } = new();
    public IPageNavigation PageNavigation { get; }

    public ObservableCollection<Player> SelectedPlayers { get; } = new();

    [Reactive]
    private bool isVisible = false;

    public CreateGameViewModel(IUnitOfWork db)
    {
        this.db = db;
        IObservable<bool> isAnyPlayerSelected = Observable
            .FromEventPattern<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
                h => SelectedPlayers.CollectionChanged += h,
                h => SelectedPlayers.CollectionChanged -= h)
            .Select(x =>
            {
                ObservableCollection<Player> selectedPlayers = (x.Sender as ObservableCollection<Player>)!;
                return selectedPlayers.Any();
            });
        
        CanStartGame = this.WhenAnyValue(x => x.SelectedGameType)
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
    private void PlayersView()
    {
        PageNavigation.GoNext<PlayersViewModel>();
    }
}
