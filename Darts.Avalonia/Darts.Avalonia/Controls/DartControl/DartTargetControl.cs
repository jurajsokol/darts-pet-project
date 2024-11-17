using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using System;
using System.Linq;
using System.Windows.Input;

namespace Darts.Avalonia.Controls.DartControl;

[TemplatePart(Name = DART_TARGET_CANVAS_NAME, Type = typeof(Grid))]
public class DartTargetControl : Button
{
    private const string DART_TARGET_CANVAS_NAME = "BackgroundButtonsCanvas";
    private DartNumbers[] dartNumberOrder = new[]
    {
        DartNumbers.Twenty,
        DartNumbers.One,
        DartNumbers.Eighteen,
        DartNumbers.Four,
        DartNumbers.Thirteen,
        DartNumbers.Six,
        DartNumbers.Ten,
        DartNumbers.Fifteen,
        DartNumbers.Two,
        DartNumbers.Seventeen,
        DartNumbers.Three,
        DartNumbers.Nineteen,
        DartNumbers.Seven,
        DartNumbers.Sixteen,
        DartNumbers.Eight,
        DartNumbers.Eleven,
        DartNumbers.Fourteen,
        DartNumbers.Nine,
        DartNumbers.Twelve,
        DartNumbers.Five,
    };
    private const int BUTTON_ANGLE = 18;

    public static DirectProperty<DartTargetControl, ICommand> ClickCommandProperty =
        AvaloniaProperty.RegisterDirect<DartTargetControl, ICommand>(nameof(ClickCommand), o => o.ClickCommand, (o, v) => o.ClickCommand = v);

    public ICommand ClickCommand
    {
        get => GetValue(ClickCommandProperty);
        set => SetValue(ClickCommandProperty, value);
    }

    public event EventHandler<DartButtonClickEventArgs> DartButtonClick;

    protected void OnDartButtonClick(DartNumbers number, DartsNumberType type)
    {
        DartButtonClick?.Invoke(this, new DartButtonClickEventArgs(number, type));
        if (ClickCommand != null)
        {
            ClickCommand.Execute((number, type));
        }
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        Grid? background = e.NameScope.Find(DART_TARGET_CANVAS_NAME) as Grid;
        if (background is not null)
        {
            foreach (var item in dartNumberOrder
                .Select((num, orderNum) => new { ButtonNumber = num, ButtonAngle = orderNum * BUTTON_ANGLE, IsButtonDark = !(orderNum % 2 == 1) }))
            {
                var button = new DartBackgroundButtonControl(
                    item.ButtonNumber,
                    item.ButtonAngle,
                    item.IsButtonDark)
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                };

                button.DartButtonClick += (sender, e) =>
                {
                    OnDartButtonClick(e.Number, e.Type);
                };

                background.Children.Add(button);
            }
        }

        Button? bullsEye = e.NameScope.Find("SingleBullsEye") as Button;
        if (bullsEye is not null)
        {
            bullsEye.Click += (sender, e) => OnDartButtonClick(DartNumbers.BullsEye, DartsNumberType.Single);
        }

        Button? doubleBullsEye = e.NameScope.Find("DoubleBullsEye") as Button;
        if (doubleBullsEye is not null)
        {
            doubleBullsEye.Click += (sender, e) => OnDartButtonClick(DartNumbers.BullsEye, DartsNumberType.Double);
        }

        Button? missButton = e.NameScope.Find("DartMissButton") as Button;
        if (missButton is not null)
        {
            missButton.Click += (sender, e) => OnDartButtonClick(DartNumbers.Miss, DartsNumberType.Single);
        }
    }
}
