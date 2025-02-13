using Darts.Avalonia.Enums;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Reactive.Linq;
using System;
using System.Reactive.Disposables;
using Darts.Avalonia.Views;

namespace Darts.Avalonia.ViewModels;

public abstract partial class KeyboardViewModel : ReactiveObject, IActivatableViewModel
{
    public ViewModelActivator Activator { get; } = new();

    [Reactive]
    private bool isDoubleSelected;

    [Reactive]
    private bool isTripleSelected;

    public KeyboardViewModel()
    {
        this.WhenActivated(disposables =>
        {
            IObservable<DartsNumberModifier> doubleObservable = this.WhenAnyValue(x => x.IsDoubleSelected)
               .Select(x => x ? DartsNumberModifier.Double : DartsNumberModifier.Single)
               .StartWith(DartsNumberModifier.Single);

            IObservable<DartsNumberModifier> tripleObservable = this.WhenAnyValue(x => x.IsTripleSelected)
                .Select(x => x ? DartsNumberModifier.Triple : DartsNumberModifier.Single)
                .StartWith(DartsNumberModifier.Single);

            IObservable<DartsNumberModifier> modifierObservable = Observable.CombineLatest(doubleObservable, tripleObservable, (x, y) => new { Double = x, Triple = y })
                .Select(x =>
                {
                    return x switch
                    {
                        { Double: DartsNumberModifier.Double, Triple: DartsNumberModifier.Single } => DartsNumberModifier.Double,
                        { Double: DartsNumberModifier.Single, Triple: DartsNumberModifier.Triple } => DartsNumberModifier.Triple,
                        _ => DartsNumberModifier.Single,
                    };
                });

            this.WhenAnyValue(x => x.IsDoubleSelected)
                .Where(x => x)
                .Subscribe(_ => IsTripleSelected = false)
                .DisposeWith(disposables);

            this.WhenAnyValue(x => x.IsTripleSelected)
                .Where(x => x)
                .Subscribe(_ => IsDoubleSelected = false)
                .DisposeWith(disposables);

            IObservable<DartNumbers> dartNumbers = OnDartNumberCommand.Publish().RefCount();

            dartNumbers
                .WithLatestFrom(modifierObservable, (number, modifier) => new DartScore() { DartNumbers = number, Modifier = modifier })
                .Subscribe(x => OnDartScore(x))
                .DisposeWith(disposables);

            dartNumbers
                .Subscribe(_ =>
                {
                    IsDoubleSelected = false;
                    IsTripleSelected = false;
                })
                .DisposeWith(disposables);
        });
    }

    [ReactiveCommand]
    public DartNumbers OnDartNumber(DartNumbers dartNumber)
    {
        return dartNumber;
    }

    internal abstract void OnDartScore(DartScore score);

    [ReactiveCommand]
    private void DoubleBullsEye()
    {
        OnDartScore(new DartScore() { DartNumbers = DartNumbers.BullsEye, Modifier = DartsNumberModifier.Double });
    }
}
