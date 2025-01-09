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
using Darts.Avalonia.Views.X01GameView;
using Darts.Avalonia.Factories;
using Darts.Games.Enums;
using Darts.Avalonia.GameScope;

namespace Darts.Avalonia;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddPageNavigation(this IServiceCollection services)
    {
        return services
            .AddScoped<PageNavigation>(s => new PageNavigation(s.GetRequiredService<MainView>().NavigationPanel, s))
            .AddScoped<IPageNavigation, PageNavigation>(s => s.GetRequiredService<PageNavigation>());
    }

    public static IServiceCollection AddDialogs(this IServiceCollection services)
    {
        return services
            .AddTransient<IDialogScope<ConfirmGameExitViewModel>, DialogScope<ConfirmGameExitViewModel>>(s => new DialogScope<ConfirmGameExitViewModel>(s.CreateScope(), s.GetRequiredService<MainView>().MainPanel))
            .AddTransient<DialogBase<ConfirmGameExitViewModel>, ConfirmGameClose>()
            .AddTransient<IDialogScope<AddPlayerViewModel>, DialogScope<AddPlayerViewModel>>(s => new DialogScope<AddPlayerViewModel>(s.CreateScope(), s.GetRequiredService<MainView>().MainPanel))
            .AddTransient<DialogBase<AddPlayerViewModel>, AddPlayerView>()
            .AddSingleton<IAbstractFactory<IDialogScope<AddPlayerViewModel>>, AbstractFactory<IDialogScope<AddPlayerViewModel>>>(s =>
            {
                return new AbstractFactory<IDialogScope<AddPlayerViewModel>>(
                    () => new DialogScope<AddPlayerViewModel>(s.CreateScope(), s.GetRequiredService<MainView>().MainPanel));
            })
            .AddSingleton<IAbstractFactory<IDialogScope<ConfirmGameExitViewModel>>, AbstractFactory<IDialogScope<ConfirmGameExitViewModel>>>(s =>
            {
                return new AbstractFactory<IDialogScope<ConfirmGameExitViewModel>>(
                    () => new DialogScope<ConfirmGameExitViewModel>(s.CreateScope(), s.GetRequiredService<MainView>().MainPanel));
            });
    }

    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        return services
            .AddSingleton<CreateGameViewModel>()
            .AddScoped<AddPlayerViewModel>()
            .AddTransient<DartGameX01ViewModel>()
            .AddScoped<X01SetupViewModel>()
            .AddTransient<PlayersViewModel>()
            .AddSingleton<ConfirmGameExitViewModel>()
            .AddTransient<SettingsViewModel>()
            .AddSingleton<MainMenuViewModel>()
            .AddTransient<CricketGameViewModel>()
            .AddTransient<WinnerViewModel>();
    }

    public static IServiceCollection AddViews(this IServiceCollection services)
    {
        return services
            .AddSingleton<MainView>()
            .AddTransient<CreateGameView>()
            .AddTransient<DartGameX01View>()
            .AddScoped<X01GameSetup>()
            .AddTransient<PlayersView>()
            .AddTransient<ConfirmGameClose>()
            .AddSingleton<MainMenuView>()
            .AddTransient<SettingsView>()
            .AddTransient<CricketGameView>()
            .AddTransient<GameWinnerView>();
    }

    public static IServiceCollection AddDartGames(this IServiceCollection services)
    {
        return services.AddTransient<X01>(s =>
            {
                GameConfiguration createGameParams = s.GetRequiredService<GameConfiguration>();
                X01SetupViewModel setupViewModel = s.GetRequiredService<X01SetupViewModel>();
                Player[] players = createGameParams.Players.Select((x, i) => x.ToDartPlayer(i)).ToArray();

                IEnumerable<Player> gamePlayers = players
                    .Select((p, i) => p with { Score = (int)setupViewModel.X01GameType.GameType, IsPlayerActive = i == 0 });
                IEnumerable<PlayerMove> moves = Enumerable
                   .Range(0, 3)
                   .Select(x => new PlayerMove(TargetButtonNum.None, TargetButtonType.None, x));

                Store<Player> store = new Store<Player>(gamePlayers.ToArray(), moves.ToArray(), new PlayerComparer());
                return new X01(players, store);
            })
            .AddTransient<CricketGame>(s =>
            {
                GameConfiguration createGameParams = s.GetRequiredService<GameConfiguration>();
                CricketPlayer[] players = createGameParams.Players.Select((x, i) => x.ToCricketPlayer(i)).ToArray();

                IEnumerable<CricketPlayer> gamePlayers = players
                    .Select((p, i) => p with { IsPlayerActive = i == 0 });
                IEnumerable<PlayerMove> moves = Enumerable
                   .Range(0, 3)
                   .Select(x => new PlayerMove(TargetButtonNum.None, TargetButtonType.None, x));

                Store<CricketPlayer> store = new Store<CricketPlayer>(gamePlayers.ToArray(), moves.ToArray(), new CricketPlayerComparer());
                return new CricketGame(players, store);
            })
            .AddTransient(services => new X01GameScope(services, services.GetRequiredService<MainView>().NavigationPanel))
            .AddTransient(services => new CricketGameScope(services, services.GetRequiredService<MainView>().NavigationPanel))
            .AddScoped<IGameScope>(services =>
            {
                GameConfiguration createGameViewModel = services.GetRequiredService<GameConfiguration>();

                return createGameViewModel.GameType switch
                {
                    GameTypes.X01 => services.GetRequiredService<X01GameScope>(),
                    GameTypes.Cricket => services.GetRequiredService<CricketGameScope>(),

                    _ => throw new NotImplementedException($"Game type {createGameViewModel.GameType} is not implemented")
                };
            })
            .AddScoped<GameConfiguration>();
    }
}
