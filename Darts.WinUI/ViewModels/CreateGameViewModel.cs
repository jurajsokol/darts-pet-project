using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Darts.DAL;
using Darts.Games.Games;
using Darts.WinUI.DependencyInjectionExtentions;
using Darts.WinUI.Models;
using Darts.WinUI.PageNavigation;
using Darts.WinUI.Views.DialogWindow;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Darts.WinUI.ViewModels
{
    public partial class CreateGameViewModel : ObservableObject, INewDartGameArgs
    {
        private IPageNavigation pageNavigation;
        private readonly IAbstractFactory<Player, IDialogWindow<Player>> playerDialogWindow;
        private readonly IUnitOfWork db;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(StartGameCommand))]
        private GameTypeModel selectedGameType;

        public ObservableCollection<Player> Players { get; } = new();

        public IList<Player> SelectedPlayers { get; set; } = Array.Empty<Player>();

        public IList<Player> GamePlayers => SelectedPlayers;
        public GameTypes GameType => SelectedGameType.GameType;

        public ObservableCollection<GameTypeModel> GameTypes { get; } = new ObservableCollection<GameTypeModel>(
            Enum.GetValues(typeof(GameTypes))
            .Cast<GameTypes>()
            .Select(x => new GameTypeModel(x)));

        public CreateGameViewModel(IPageNavigation pageNavigation, IAbstractFactory<Player, IDialogWindow<Player>> playerDialogWindow, IUnitOfWork db)
        {
            this.pageNavigation = pageNavigation;
            this.playerDialogWindow = playerDialogWindow;
            this.db = db;
            Players.CollectionChanged += (o, args) => StartGameCommand.NotifyCanExecuteChanged();
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
            pageNavigation.SetPage<EditPlayersViewModel>();
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
            pageNavigation.SetPage<DartsGameViewModel>();
        }

        private bool CanStartGame()
        {
            return Players.Any();
        }
    }
}
