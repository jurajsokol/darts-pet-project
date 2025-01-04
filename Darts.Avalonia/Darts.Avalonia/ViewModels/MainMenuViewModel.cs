using ReactiveUI;
using System;
using System.Reactive.Disposables;

namespace Darts.Avalonia.ViewModels;

public partial class MainMenuViewModel : ReactiveObject, IActivatableViewModel
{
    private readonly IServiceProvider serviceProvider;

    public ViewModelActivator Activator { get; } = new ViewModelActivator();

    public MainMenuViewModel(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
        this.WhenActivated((CompositeDisposable disposable) =>
        {
        });
    }
}
