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
using Darts.Avalonia.Factories;
using static System.Formats.Asn1.AsnWriter;

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
            .AddTransient<SettingsViewModel>();
    }

    public static IServiceCollection AddViews(this IServiceCollection services)
    {
        return services
            .AddSingleton<MainView>()
            .AddTransient<CreateGameView>()
            .AddScoped<DartGameX01View>()
            .AddScoped<X01GameSetup>()
            .AddTransient<PlayersView>()
            .AddTransient<ConfirmGameClose>()
            .AddTransient<MainMenuView>()
            .AddTransient<SettingsView>();
    }

    public static IServiceCollection AddDartGames(this IServiceCollection services)
    {
        return services.AddScoped<IDartGame, X01>(s =>
            {
                CreateGameViewModel createGameParams = s.GetRequiredService<CreateGameViewModel>();
                X01SetupViewModel setupViewModel = s.GetRequiredService<X01SetupViewModel>();
                Player[] players = createGameParams.SelectedPlayers.Select((x, i) => x.ToDartPlayer(i)).ToArray();

                IEnumerable<Player> gamePlayers = players
                    .Select((p, i) => p with { Score = (int)setupViewModel.X01GameType.GameType, IsPlayerActive = i == 0 });
                IEnumerable<PlayerMove> moves = Enumerable
                   .Range(0, 3)
                   .Select(x => new PlayerMove(TargetButtonNum.None, TargetButtonType.None, x));

                Store store = new Store(gamePlayers.ToArray(), moves.ToArray());

                return createGameParams.SelectedGameType.GameType switch
                {
                    GameTypes.X01 => new X01(players, store),

                    _ => throw new NotImplementedException($"Game type {createGameParams.SelectedGameType.GameType} is not implemented yet"),
                };
            })
            .AddScoped(services => new GameScope(services, services.GetRequiredService<MainView>().NavigationPanel))
            .AddSingleton<IAbstractFactory<GameScope>>(services =>
            {
                return new AbstractFactory<GameScope>(() =>
                {
                    IServiceScope scope = services.CreateScope();

                    GameScope gameScope = scope.ServiceProvider.GetRequiredService<GameScope>();
                    gameScope.Disposables.Add(scope);
                    return gameScope;
                });
            });
    }
}
