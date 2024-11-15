using Darts.Avalonia.ViewModels;
using ReactiveUI;
using System;
using System.Linq;

namespace Darts.Avalonia.ViewRouting;

public class AppViewLocator : IViewLocator
{
    public IViewFor? ResolveView<T>(T? viewModel, string? contract = null)
    {
        Type? genericType = viewModel?.GetType().GetGenericArguments().FirstOrDefault();

        return genericType switch
        {
            Type t when t == typeof(CreateGameViewModel) => new CreateGameView(),
            Type t when t == typeof(AddPlayerViewModel) => new AddPlayerView(),
            _ => null
        };
    }
}
