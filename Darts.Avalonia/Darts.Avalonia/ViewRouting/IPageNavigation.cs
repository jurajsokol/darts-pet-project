using ReactiveUI;
using System;
using System.Reactive;

namespace Darts.Avalonia.ViewRouting
{
    public interface IPageNavigation
    {
        public ReactiveCommand<Type, Unit> GoNextCommand { get; }
        public ReactiveCommand<Unit, Unit> GoBackCommand { get; }
        void GoNext<T>() where T : ReactiveObject;
        void GoBack();
    }
}
