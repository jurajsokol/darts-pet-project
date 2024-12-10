using Darts.Avalonia.ViewModels;
using Darts.Avalonia.ViewRouting;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;

namespace Darts.Avalonia;

public class GameScope : IDisposable
{
    private readonly IServiceScope serviceScope;
    private readonly PageNavigation pageNavigation;
    private readonly PageNavigation rootPageNavigation;

    public GameScope(IServiceScope serviceScope, PageNavigation pageNavigation, PageNavigation rootPageNavigation)
    {
        this.serviceScope = serviceScope;
        this.pageNavigation = pageNavigation;
        this.rootPageNavigation = rootPageNavigation;
    }

    public void StartGame()
    {
        pageNavigation.GoNext<X01SetupViewModel>();
    }

    public void Dispose()
    {
        rootPageNavigation.GoToRootPage();
        serviceScope.Dispose();
    }

    public void GoTo<T>() where T : ReactiveObject
    {
        pageNavigation.GoNext<T>();
    }
}
