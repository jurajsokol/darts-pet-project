using ReactiveUI;
using System.Linq;
using System.Reflection;
using ReactiveUI.SourceGenerators;
using Avalonia.Styling;
using System;
using System.Reactive.Disposables;

namespace Darts.Avalonia.ViewModels;

public partial class SettingsViewModel : ReactiveObject, IActivatableViewModel
{
    public string Revision { get; }
    public string BuildDate { get; }

    public ViewModelActivator Activator { get; }

    [Reactive]
    private bool isDarkTheme;

    public SettingsViewModel()
    {
        Activator = new ViewModelActivator();

        AssemblyMetadataAttribute[] assemblyAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes<AssemblyMetadataAttribute>().ToArray();

        Revision = assemblyAttributes.FirstOrDefault(x => x.Key == "RevisionCount")?.Value ?? string.Empty;
        BuildDate = assemblyAttributes.FirstOrDefault(x => x.Key == "BuildDate")?.Value ?? string.Empty;

        IsDarkTheme = (string)App.Current.ActualThemeVariant.Key == "Dark";

        this.WhenActivated((CompositeDisposable disposable) =>
        {
            this.WhenAnyValue(x => x.IsDarkTheme).Subscribe(x =>
            {
                if (x)
                {
                    App.Current.RequestedThemeVariant = new ThemeVariant("Dark", null);
                }
                else
                {
                    App.Current.RequestedThemeVariant = new ThemeVariant("Light", null);
                }
            })
            .DisposeWith(disposable);
        });

    }


}

