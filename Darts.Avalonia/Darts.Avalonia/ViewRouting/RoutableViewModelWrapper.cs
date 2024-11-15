using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;

namespace Darts.Avalonia.ViewRouting
{
    public class RoutableViewModelWrapper<T> : ReactiveObject, IRoutableViewModel where T : ReactiveObject
    {
        private readonly IServiceProvider services;

        public string? UrlPathSegment => nameof(T);

        public IScreen HostScreen { get; }

        public T ViewModel { get; }

        public RoutableViewModelWrapper(IScreen hostScreen, IServiceProvider services)
        {
            HostScreen = hostScreen;
            this.services = services;
            ViewModel = services.GetRequiredService<T>();
        }
    }
}
