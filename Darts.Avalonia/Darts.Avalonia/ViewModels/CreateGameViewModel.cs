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

namespace Darts.Avalonia.ViewModels;

public partial class CreateGameViewModel : ReactiveObject
{
    private readonly IUnitOfWork db;

    [Reactive]
    private GameTypeModel selectedGameType;

    public ObservableCollection<GameTypeModel> GameTypes { get; } = new ObservableCollection<GameTypeModel>(
        Enum.GetValues(typeof(GameTypes))
        .Cast<GameTypes>()
        .Select(x => new GameTypeModel(x)));

    public ObservableCollection<Player> Players { get; } = new();

    [Reactive]
    private IList<Player> selectedPlayers = Array.Empty<Player>();

    [Reactive]
    private bool isVisible = false;

    public CreateGameViewModel(IUnitOfWork db)
    {
        this.db = db;
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
}
