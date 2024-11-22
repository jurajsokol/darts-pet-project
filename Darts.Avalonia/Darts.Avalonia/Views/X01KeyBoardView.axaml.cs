using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Darts.Avalonia.Enums;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Input;

namespace Darts.Avalonia.Views;

public partial class X01KeyBoardView : UserControl, IActivatableView
{
    public static readonly StyledProperty<ICommand?> CommandProperty = AvaloniaProperty
        .Register<X01KeyBoardView, ICommand?>(nameof(Command));

    public ICommand? Command
    {
        get => GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public X01KeyBoardView()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            IObservable<DartsNumberModifier> doubleModifierObservable = DoubleButton.GetObservable(ToggleButton.ClickEvent)
                .Select(x =>
                {
                    bool isChecked = (x.Source as ToggleButton)?.IsChecked ?? false;
                    if (isChecked)
                    {
                        return DartsNumberModifier.Double;
                    }

                    return DartsNumberModifier.Single;
                })
                .Publish()
                .RefCount();

            IObservable<DartsNumberModifier> tripleModifierObservable = TripleButton.GetObservable(ToggleButton.ClickEvent)
                .Select(x =>
                {
                    bool isChecked = (x.Source as ToggleButton)?.IsChecked ?? false;
                    if (isChecked)
                    {
                        return DartsNumberModifier.Triple;
                    }

                    return DartsNumberModifier.Single;
                })
                .Publish()
                .RefCount();

            doubleModifierObservable
                .Where(x => x == DartsNumberModifier.Double)
                .Subscribe(data =>
                {
                    DoubleButton.IsChecked = true;
                    TripleButton.IsChecked = false;
                })
                .DisposeWith(disposables);

            tripleModifierObservable
                .Where(x => x == DartsNumberModifier.Triple)
                .Subscribe(data =>
                {
                    TripleButton.IsChecked = true;
                    DoubleButton.IsChecked = false;
                })
                .DisposeWith(disposables);

            IObservable<DartsNumberModifier> modifierObservable = Observable
                .Merge(
                    doubleModifierObservable,
                    tripleModifierObservable)
                .StartWith(DartsNumberModifier.Single);

            Observable
                .Merge(
                    OneButton.GetObservable(Button.ClickEvent).Select(_ => DartNumbers.One),
                    TwoButton.GetObservable(Button.ClickEvent).Select(_ => DartNumbers.Two),
                    ThreeButton.GetObservable(Button.ClickEvent).Select(_ => DartNumbers.Three),
                    FourButton.GetObservable(Button.ClickEvent).Select(_ => DartNumbers.Four),
                    FiveButton.GetObservable(Button.ClickEvent).Select(_ => DartNumbers.Five),
                    SixButton.GetObservable(Button.ClickEvent).Select(_ => DartNumbers.Six),
                    SevenButton.GetObservable(Button.ClickEvent).Select(_ => DartNumbers.Seven),
                    EightButton.GetObservable(Button.ClickEvent).Select(_ => DartNumbers.Eight),
                    NineButton.GetObservable(Button.ClickEvent).Select(_ => DartNumbers.Nine),
                    TenButton.GetObservable(Button.ClickEvent).Select(_ => DartNumbers.Ten),
                    ElevenButton.GetObservable(Button.ClickEvent).Select(_ => DartNumbers.Eleven),
                    TwelveButton.GetObservable(Button.ClickEvent).Select(_ => DartNumbers.Twelve),
                    ThirteenButton.GetObservable(Button.ClickEvent).Select(_ => DartNumbers.Thirteen),
                    FourteenButton.GetObservable(Button.ClickEvent).Select(_ => DartNumbers.Fourteen),
                    FifteenButton.GetObservable(Button.ClickEvent).Select(_ => DartNumbers.Fifteen),
                    SixteenButton.GetObservable(Button.ClickEvent).Select(_ => DartNumbers.Sixteen),
                    SeventeenButton.GetObservable(Button.ClickEvent).Select(_ => DartNumbers.Seventeen),
                    EighteenButton.GetObservable(Button.ClickEvent).Select(_ => DartNumbers.Eighteen),
                    NineteenButton.GetObservable(Button.ClickEvent).Select(_ => DartNumbers.Nineteen),
                    TwentyButton.GetObservable(Button.ClickEvent).Select(_ => DartNumbers.Twenty))
                .WithLatestFrom(modifierObservable, (number, modifier) => new DartScore() { DartNumbers = number, Modifier = modifier })
                .Merge(
                    Observable.Merge(
                        BullsEyeButton.GetObservable(Button.ClickEvent)
                            .Select(_ => new DartScore() { DartNumbers = DartNumbers.BullsEye, Modifier = DartsNumberModifier.Single }),
                        DoubleBullsEyeButton.GetObservable(Button.ClickEvent)
                            .Select(_ => new DartScore() { DartNumbers = DartNumbers.DoubleBullsEye, Modifier = DartsNumberModifier.Single })))
                .Subscribe(x => Command?.Execute(x))
                .DisposeWith(disposables);
        });
    }
}