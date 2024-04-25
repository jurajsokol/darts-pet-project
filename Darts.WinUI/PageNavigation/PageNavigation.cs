using Darts.WinUI.ViewModels;
using Darts.WinUI.Views;
using Microsoft.UI.Xaml.Controls;
using System;

namespace Darts.WinUI.PageNavigation
{
    public interface IPageNavigation
    {
        void SetPage<T>();
    }

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
}
