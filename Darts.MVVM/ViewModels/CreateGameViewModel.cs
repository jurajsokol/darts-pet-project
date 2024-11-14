using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Darts.DAL;
using Darts.Games.Games;
using Darts.MVVM.DependencyInjectionExtentions;
using Darts.MVVM.Models;
using System.Collections.ObjectModel;

namespace Darts.MVVM.ViewModels;

public partial class CreateGameViewModel : ObservableObject
{
    private readonly IAbstractFactory<Player, IDialogWindow<Player>> playerDialogWindow;
    private readonly IUnitOfWork db;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(StartGameCommand))]
    private GameTypeModel selectedGameType;

    public ObservableCollection<Player> Players { get; } = new();

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(StartGameCommand))]
    private IList<Player> selectedPlayers = Array.Empty<Player>();

    [ObservableProperty]
    private bool isVisible = false;

    public GameTypes GameType => SelectedGameType.GameType;

    public ObservableCollection<GameTypeModel> GameTypes { get; } = new ObservableCollection<GameTypeModel>(
        Enum.GetValues(typeof(GameTypes))
        .Cast<GameTypes>()
        .Select(x => new GameTypeModel(x)));

    public CreateGameViewModel(IUnitOfWork db)
    {
        this.db = db;
        Players.CollectionChanged += (o, args) => StartGameCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand]
    private void SetVisisbility()
    {
        IsVisible = !IsVisible;
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

    [RelayCommand]
    private void EditPlayer()
    {
    }

    [RelayCommand]
    private async Task AddUser()
    {
        var dialog = playerDialogWindow.Create();
        if (await dialog.ShowDialog())
        {
            var addedPlayer = await db.Players.Add(dialog.ViewModel.ToDalPLayer());
            await db.CompleteAsync();
            Players.Add(addedPlayer.ToModel());
        }
    }

    public void ReorderPlayers()
    {
        foreach (var player in Players.Select((player, n) => new { player, n }))
        {
            player.player.OrderNumber = player.n +1;
        }
    }

    [RelayCommand(CanExecute = nameof(CanStartGame))]
    private void StartGame()
    {
    }

    private bool CanStartGame()
    {
        return (SelectedPlayers?.Any() ?? false) && (SelectedGameType is not null);
    }
}
