using Darts.Avalonia.ViewModels;
using Darts.Avalonia.ViewRouting;
using Darts.Avalonia.Views;
using Darts.Avalonia.Views.Dialog;
using Darts.Games.Games;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Darts.Games.Models;
using Darts.Games.State;
using System.Collections.Generic;
using Darts.Games;
using Darts.Avalonia.Views.X01GameView;

namespace Darts.Avalonia;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddPageNavigation(this IServiceCollection services)
    {
        return services
            .AddSingleton<PageNavigation>(s => new PageNavigation(s.GetRequiredService<MainView>().NavigationPanel, s))
            .AddSingleton<IPageNavigation, PageNavigation>(s => s.GetRequiredService<PageNavigation>());
    }

    public static IServiceCollection AddDialogs(this IServiceCollection services)
    {
        return services
            .AddTransient<IDialogScope<AddPlayerViewModel>, DialogScope<AddPlayerViewModel>>(s => new DialogScope<AddPlayerViewModel>(s.CreateScope(), s.GetRequiredService<MainView>().MainPanel))
            .AddTransient<DialogBase<AddPlayerViewModel>, AddPlayerView>()
            .AddSingleton<IDialogManager, DialogManager>(s => new DialogManager(s.GetRequiredService<MainView>().MainPanel, s));
    }

    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        return services
            .AddSingleton<CreateGameViewModel>()
            .AddScoped<AddPlayerViewModel>()
            .AddTransient<DartGameX01ViewModel>()
            .AddScoped<X01SetupViewModel>()
            .AddTransient<PlayersViewModel>();
    }

    public static IServiceCollection AddViews(this IServiceCollection services)
    {
        return services
            .AddSingleton<MainView>()
            .AddTransient<CreateGameView>()
            .AddTransient<DartGameX01View>()
            .AddTransient<X01GameSetup>()
            .AddTransient<PlayersView>();
    }

    public static IServiceCollection AddGameLogic(this IServiceCollection services)
    {
        return services
            .AddTransient<IDartGame, X01>();
    }

    public static IServiceCollection AddDartGames(this IServiceCollection services)
    {
        return services.AddTransient<IDartGame, X01>(s =>
        {
            CreateGameViewModel createGameParams = s.GetRequiredService<CreateGameViewModel>();
            Player[] players = createGameParams.SelectedPlayers.Select((x, i) => x.ToDartPlayer(i)).ToArray();

            IEnumerable<Player> gamePlayers = players
                .Select((p, i) => p with { Score = (int)createGameParams.SelectedGameType.GameType, IsPlayerActive = i == 0 });
            IEnumerable<PlayerMove> moves = Enumerable
               .Range(0, 3)
               .Select(x => new PlayerMove(TargetButtonNum.None, TargetButtonType.None, x));


            Store store = new Store(gamePlayers.ToArray(), moves.ToArray());

            return createGameParams.SelectedGameType.GameType switch
            {
                //X01GameTypes._301 => new X01(players, store),
                //X01GameTypes._401 => new X01(players, store),
                //X01GameTypes._501 => new X01(players, store),
                //X01GameTypes._601 => new X01(players, store),

                _ => throw new NotImplementedException($"Game type {createGameParams.SelectedGameType.GameType} is not implemented yet"),
            };
        });
    }
}
