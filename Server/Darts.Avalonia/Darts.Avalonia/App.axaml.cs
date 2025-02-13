using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Darts.Avalonia.Views;
using Darts.DAL;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;
using System.IO;
using System.Reactive.Concurrency;

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
        var services = new ServiceCollection();

        return services.AddDatabase(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "darts.db"))
            .AddSingleton<IScheduler>(_ => RxApp.MainThreadScheduler)
            .AddPageNavigation()
            .AddViewModels()
            .AddDialogs()
            .AddViews()
            .AddDartGames()
            .BuildServiceProvider();
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