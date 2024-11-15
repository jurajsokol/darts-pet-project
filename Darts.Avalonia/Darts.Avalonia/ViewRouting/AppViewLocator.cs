using Darts.Avalonia.ViewModels;
using ReactiveUI;
using System;

namespace Darts.Avalonia.ViewRouting;

public class AppViewLocator : IViewLocator
{
    public IViewFor? ResolveView<T>(T? viewModel, string? contract = null)
    {
        return viewModel switch
        {
            CreateGameViewModel context => new CreateGameView(context),
            AddPlayerViewModel context => new AddPlayerView(),
            _ => throw new NotImplementedException($"Cannot locate view for {typeof(T).Name}")
        };
    }
}
