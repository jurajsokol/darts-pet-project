using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using Darts.Avalonia.Enums;
using Darts.Avalonia.ViewModels;
using Darts.Avalonia.Views;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Darts.Avalonia;

public partial class CricketGameView : ReactiveUserControl<CricketGameViewModel>
{
    public ToggleButton DoubleButton => this.FindControl<ToggleButton>("Double")!;
    public ToggleButton TripleButton => this.FindControl<ToggleButton>("Tripple")!;
    public Button FifteenButton => this.FindControl<Button>("Fifteen")!;
    public Button SixteenButton => this.FindControl<Button>("Sixteen")!;
    public Button SeventeenButton => this.FindControl<Button>("Seventeen")!;
    public Button EighteenButton => this.FindControl<Button>("Eighteen")!;
    public Button NineteenButton => this.FindControl<Button>("Nineteen")!;
    public Button TwentyButton => this.FindControl<Button>("Twenty")!;
    public Button BullsEyeButton => this.FindControl<Button>("BullsEye")!;
    public Button MissButton => this.FindControl<Button>("Miss")!;

    public CricketGameView(CricketGameViewModel viewModel)
    {
        DataContext = viewModel;
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
                   FifteenButton.GetObservable(Button.ClickEvent).Select(_ => DartNumbers.Fifteen),
                   SixteenButton.GetObservable(Button.ClickEvent).Select(_ => DartNumbers.Sixteen),
                   SeventeenButton.GetObservable(Button.ClickEvent).Select(_ => DartNumbers.Seventeen),
                   EighteenButton.GetObservable(Button.ClickEvent).Select(_ => DartNumbers.Eighteen),
                   NineteenButton.GetObservable(Button.ClickEvent).Select(_ => DartNumbers.Nineteen),
                   TwentyButton.GetObservable(Button.ClickEvent).Select(_ => DartNumbers.Twenty),
                   BullsEyeButton.GetObservable(Button.ClickEvent).Select(_ => DartNumbers.BullsEye),
                   MissButton.GetObservable(Button.ClickEvent).Select(_ => DartNumbers.Miss))
               .WithLatestFrom(modifierObservable, (number, modifier) => new DartScore() { DartNumbers = number, Modifier = modifier })
               .Subscribe(x => ViewModel!.DartClick(new DartScore() { DartNumbers = x.DartNumbers, Modifier = x.Modifier }))
               .DisposeWith(disposables);
        });
    }
}