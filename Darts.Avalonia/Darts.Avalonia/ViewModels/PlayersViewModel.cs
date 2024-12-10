using Darts.Avalonia.Factories;
using Darts.Avalonia.Models;
using Darts.Avalonia.ViewRouting;
using Darts.Avalonia.Views.Dialog;
using Darts.DAL;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Darts.Avalonia.ViewModels;

public partial class PlayersViewModel : ReactiveObject
{
    private readonly IUnitOfWork db;
    private readonly IAbstractFactory<IDialogScope<AddPlayerViewModel>> dialogFactory;

    public ObservableCollection<Player> Players { get; } = new();
    public IPageNavigation PageNavigation { get; }

    public ObservableCollection<Player> SelectedPlayers { get; } = new();

    public PlayersViewModel(IPageNavigation pageNavigation, IUnitOfWork db, IAbstractFactory<IDialogScope<AddPlayerViewModel>> dialogFactory)
    {
        PageNavigation = pageNavigation;
        this.db = db;
        this.dialogFactory = dialogFactory;
    }

    [ReactiveCommand]
    public void GoBack()
    { 
        PageNavigation.GoBack();
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
    private async Task AddPlayer()
    {
        using IDialogScope<AddPlayerViewModel> scope = dialogFactory.Create();

        (DialogResult, AddPlayerViewModel) data = await scope.ShowDialog();
        if (data.Item1 == DialogResult.Ok)
        {
            await db.Players.Add(new DAL.Entities.Player() { Name = data.Item2.Name });
            await db.CompleteAsync();
            await LoadPlayers();
        }
    }

    [ReactiveCommand]
    private async Task EditPlayer()
    {
        Player? selectedPlayer = SelectedPlayers.FirstOrDefault();

        if (selectedPlayer is not null)
        {
            using IDialogScope<AddPlayerViewModel> scope = dialogFactory.Create();

            scope.ViewModel.Name = selectedPlayer.Name;

            (DialogResult, AddPlayerViewModel) data = await scope.ShowDialog();
            if (data.Item1 == DialogResult.Ok)
            {
                db.Players.Update(new DAL.Entities.Player() { ID = selectedPlayer.ID, Name = data.Item2.Name });
                await db.CompleteAsync();
                await LoadPlayers();
            }
        }
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
}
