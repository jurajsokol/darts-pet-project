using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Darts.Avalonia.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Reactive.Concurrency;
using System;
using Darts.DAL;
using Darts.Avalonia.ViewModels;
using Darts.Avalonia.Views.Dialog;
using Darts.Avalonia.ViewRouting;
using Darts.Games.Games;

namespace Darts.Avalonia;

public partial class App : Application
{
    public static IServiceProvider? Services { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        Services = ConfigureServices();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        MainView mainView = Services!.GetRequiredService<MainView>();
        Services!.GetRequiredService<PageNavigation>().SetFirstView<CreateGameViewModel>();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow() { Content = mainView };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = mainView;
        }

        base.OnFrameworkInitializationCompleted();

        CreateDB(Services!);
    }

    /// <summary>
    /// Configures the services for the application.
    /// </summary>
    private static IServiceProvider ConfigureServices()
    {
        CurrentThreadScheduler guiScheduler = Scheduler.CurrentThread;

        var services = new ServiceCollection();

        services.AddDatabase("./darts.db")
            .AddPageNavigation()
            .AddSingleton<MainView>()
            .AddTransient<CreateGameView>()
            .AddTransient<DialogBase<AddPlayerViewModel>, AddPlayerView>()
            .AddSingleton<CreateGameViewModel>()
            .AddSingleton<AddPlayerViewModel>()
            .AddTransient<DartGameX01View>()
            .AddTransient<DartGameX01ViewModel>()
            .AddSingleton<IDialogManager, DialogManager>(s => new DialogManager(s.GetRequiredService<MainView>().MainPanel, s));

        return services.BuildServiceProvider();
    }

    private void CreateDB(IServiceProvider services)
    {
        using (var serviceScope = services.GetService<IServiceScopeFactory>()!.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetRequiredService<DartsDBContext>();
            context.Database.EnsureCreated();
        }
    }
}