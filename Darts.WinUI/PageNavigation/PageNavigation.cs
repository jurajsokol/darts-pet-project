using Darts.WinUI.Views;
using Microsoft.UI.Xaml.Controls;
using System;

namespace Darts.WinUI.PageNavigation
{
    public interface IPageNavigation
    {
        void SetPage(PageEnums page);
    }

    public class PageNavigation : IPageNavigation
    {
        public Frame Frame { get; }

        public PageNavigation(Frame frame)
        { 
            Frame = frame;
        }

        public void SetPage(PageEnums page)
        {
            Type pageType = page switch
            { 
                PageEnums.DartGamePage => typeof(DartGamePage),
                PageEnums.CreateGamePage => typeof(CreateGamePage),

                _ => throw new NotImplementedException(),
            };
            Frame.Navigate(typeof(DartGamePage));
        }
    }
}
