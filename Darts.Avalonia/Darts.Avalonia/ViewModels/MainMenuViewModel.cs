using Darts.Avalonia.Factories;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System;
using System.Reactive.Disposables;

namespace Darts.Avalonia.ViewModels;

public partial class MainMenuViewModel : ReactiveObject, IActivatableViewModel
{
    private readonly IServiceProvider serviceProvider;
    private readonly IAbstractFactory<GameScope> gameScopeFactory;
    private IDisposable gameScope = Disposable.Empty;

    public ViewModelActivator Activator { get; } = new ViewModelActivator();

    public MainMenuViewModel(IServiceProvider serviceProvider, IAbstractFactory<GameScope> gameScopeFactory)
    {
        this.serviceProvider = serviceProvider;
        this.gameScopeFactory = gameScopeFactory;
        this.WhenActivated((CompositeDisposable disposable) =>
        {
            gameScope.Dispose();
        });
    }

    [ReactiveCommand]
    private void StartGame()
    {
        IServiceScope scope = serviceProvider.CreateScope();
        gameScope = scope;
        GameScope game = scope.ServiceProvider.GetRequiredService<GameScope>();
        game.StartSetup();
    }
}
