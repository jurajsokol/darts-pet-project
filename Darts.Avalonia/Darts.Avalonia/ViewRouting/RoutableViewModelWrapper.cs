using ReactiveUI;

namespace Darts.Avalonia.ViewRouting
{
    public class RoutableViewModelWrapper<T> : ReactiveObject, IRoutableViewModel where T : ReactiveObject
    {
        public string? UrlPathSegment => nameof(T);

        public IScreen HostScreen { get; }

        public T ViewModel { get; }

        public RoutableViewModelWrapper(IScreen hostScreen, T viewModel)
        {
            HostScreen = hostScreen;
            ViewModel = viewModel;
        }
    }
}
