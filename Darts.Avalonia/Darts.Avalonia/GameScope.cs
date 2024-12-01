using Darts.Avalonia.ViewRouting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Darts.Avalonia;

public class GameScope : IDisposable
{
    private readonly IServiceScope serviceScope;
    private readonly PageNavigation pageNavigation;

    public GameScope(IServiceScope serviceScope, PageNavigation pageNavigation)
    {
        this.serviceScope = serviceScope;
        this.pageNavigation = pageNavigation;
    }


    
    public void Dispose()
    {
        serviceScope.Dispose();
    }

}
