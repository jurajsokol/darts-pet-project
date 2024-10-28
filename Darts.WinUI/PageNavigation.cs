using Darts.MVVM.PageNavigation;
using Darts.MVVM.ViewModels;
using Darts.MVVM.Views;
using Microsoft.UI.Xaml.Controls;
using System;

namespace Darts.WinUI;

public class PageNavigation : IPageNavigation
{
    public Frame Frame { get; }

    public PageNavigation(Frame frame)
    {
        Frame = frame;
    }

    public void SetPage<T>()
    {
        Type pageType = typeof(T) switch
        {
            Type t when t == typeof(DartsGameViewModel) => typeof(DartGamePage),
            Type t when t == typeof(CreateGameViewModel) => typeof(CreateGamePage),
            Type t when t == typeof(EditPlayersViewModel) => typeof(EditPlayersPage),

            _ => throw new NotImplementedException(),
        };
        Frame.Navigate(pageType);
    }
}
