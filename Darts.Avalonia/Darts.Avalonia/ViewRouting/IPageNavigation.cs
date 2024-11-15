using ReactiveUI;
using System.Reactive;

namespace Darts.Avalonia.ViewRouting
{
    public interface IPageNavigation
    {
        public ReactiveCommand<Unit, IRoutableViewModel> GoNextCommand { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> GoBackCommand { get; }
        void GoNext<T>() where T : ReactiveObject;
        void GoBack();
    }
}
