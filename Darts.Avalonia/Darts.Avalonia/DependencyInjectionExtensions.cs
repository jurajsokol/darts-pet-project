using Darts.Avalonia.ViewModels;
using Darts.Avalonia.ViewRouting;
using Darts.Avalonia.Views;
using Darts.Avalonia.Views.Dialog;
using Microsoft.Extensions.DependencyInjection;

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
            .AddTransient<DialogBase<AddPlayerViewModel>, AddPlayerView>()
            .AddSingleton<IDialogManager, DialogManager>(s => new DialogManager(s.GetRequiredService<MainView>().MainPanel, s));
    }

    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        return services
            .AddSingleton<CreateGameViewModel>()
            .AddSingleton<AddPlayerViewModel>()
            .AddTransient<DartGameX01ViewModel>();
    }

    public static IServiceCollection AddViews(this IServiceCollection services)
    {
        return services
            .AddSingleton<MainView>()
            .AddTransient<CreateGameView>()
            .AddTransient<DartGameX01View>();
    }
}
