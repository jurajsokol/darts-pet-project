using Darts.Avalonia.ViewRouting;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;

namespace Darts.Avalonia.ViewModels;

public class GameScopeViewModel : ReactiveObject, IDisposable
{
    private readonly IServiceScope serviceScope;
    private readonly PageNavigation pageNavigation;

    GameScopeViewModel(IServiceScope serviceScope, PageNavigation pageNavigation)
    {
        this.serviceScope = serviceScope;
        this.pageNavigation = pageNavigation;
    }

    public void Dispose()
    {
        pageNavigation.GoBack();
        serviceScope.Dispose();
    }
}
