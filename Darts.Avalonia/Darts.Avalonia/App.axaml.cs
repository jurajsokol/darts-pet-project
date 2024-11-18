using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Darts.Avalonia.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using Darts.DAL;
using Darts.Avalonia.ViewModels;
using Darts.Avalonia.ViewRouting;

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
        var services = new ServiceCollection();

        return services.AddDatabase("./darts.db")
            .AddPageNavigation()
            .AddViewModels()
            .AddDialogs()
            .AddViews()
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