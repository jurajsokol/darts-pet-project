using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media;
using Darts.Avalonia.Enums;
using System;

namespace Darts.Avalonia.Controls.DartControl;

[TemplatePart(Name = "BackgroundButtonElement", Type = typeof(Button))]
[TemplatePart(Name = "DartDoubleButtonElement", Type = typeof(Button))]
[TemplatePart(Name = "DartTrippleButtonElement", Type = typeof(Button))]
public class DartBackgroundButtonControl : Button
{
    private DartNumbers buttonNumber;

    public int ButtonAngle { get; }

    public static readonly RoutedEvent<RoutedEventArgs> BackgroundButtonClickEvent =
        RoutedEvent.Register<DartBackgroundButtonControl, RoutedEventArgs>(nameof(BackgroundButtonClick), RoutingStrategies.Direct);

    public event EventHandler<RoutedEventArgs> BackgroundButtonClick
    {
        add => AddHandler(BackgroundButtonClickEvent, value);
        remove => RemoveHandler(BackgroundButtonClickEvent, value);
    }


    private Button? backgroundButtonElement;
    private Button? BackgroundButtonElement
    {
        get => backgroundButtonElement;

        set
        {
            if (backgroundButtonElement is not null)
            {
                backgroundButtonElement.Click -= DartBackgroundButton_Click;
            }
            backgroundButtonElement = value;

            if (backgroundButtonElement is not null)
            {
                backgroundButtonElement.Click += DartBackgroundButton_Click;
            }
        }
    }

    public static readonly RoutedEvent<RoutedEventArgs> DartDoubleButtonClickEvent =
        RoutedEvent.Register<DartBackgroundButtonControl, RoutedEventArgs>(nameof(DartDoubleButton), RoutingStrategies.Direct);

    public event EventHandler<RoutedEventArgs> DartDoubleButton
    {
        add => AddHandler(DartDoubleButtonClickEvent, value);
        remove => RemoveHandler(DartDoubleButtonClickEvent, value);
    }

    private Button? dartDoubleButtonElement;
    private Button? DartDoubleButtonElement
    {
        get => dartDoubleButtonElement;

        set
        {
            if (dartDoubleButtonElement is not null)
            {
                dartDoubleButtonElement.Click -= DartDoubleButton_Click;
            }
            dartDoubleButtonElement = value;

            if (dartDoubleButtonElement is not null)
            {
                dartDoubleButtonElement.Click += DartDoubleButton_Click;
            }
        }
    }

    public static readonly RoutedEvent<RoutedEventArgs> DartTrippleButtonClickEvent =
        RoutedEvent.Register<DartBackgroundButtonControl, RoutedEventArgs>(nameof(DartTrippleButtonClick), RoutingStrategies.Direct);

    public event EventHandler<RoutedEventArgs> DartTrippleButtonClick
    {
        add => AddHandler(DartTrippleButtonClickEvent, value);
        remove => RemoveHandler(DartTrippleButtonClickEvent, value);
    }

    private Button? dartTrippleButtonElement;
    private Button? DartTrippleButtonElement
    {
        get => dartTrippleButtonElement;

        set
        {
            if (dartTrippleButtonElement is not null)
            {
                dartTrippleButtonElement.Click -= DartTrippleButtonElement_Click;
            }
            dartTrippleButtonElement = value;

            if (dartTrippleButtonElement is not null)
            {
                dartTrippleButtonElement.Click += DartTrippleButtonElement_Click;
            }
        }
    }

    public static readonly StyledProperty<int> TextAngleProperty = 
        AvaloniaProperty.Register<DartBackgroundButtonControl, int>(nameof(TextAngle), defaultValue: 0);

    public int TextAngle
    {
        get => GetValue(TextAngleProperty);
        set => SetValue(TextAngleProperty, value);
    }

    public static readonly StyledProperty<bool> IsButtonDarkProperty = 
        AvaloniaProperty.Register<DartBackgroundButtonControl, bool>(nameof(IsButtonDark));

    public bool IsButtonDark
    {
        get => GetValue(IsButtonDarkProperty);
        set => SetValue(IsButtonDarkProperty, value);
    }

    public static readonly DirectProperty<DartBackgroundButtonControl, int> ButtonNumberProperty = 
        AvaloniaProperty.RegisterDirect<DartBackgroundButtonControl, int>(nameof(ButtonNumber), o => o.ButtonNumber, (o, v) => o.ButtonNumber = v);

    private int buttonNumberValue;
    public int ButtonNumber
    {
        get => buttonNumberValue;
        set => SetAndRaise(ButtonNumberProperty, ref buttonNumberValue, value);
    }

    public event EventHandler<DartButtonClickEventArgs>? DartButtonClick;

    protected void OnDartButtonClick(DartNumbers number, DartsNumberModifier type)
    {
        DartButtonClick?.Invoke(this, new DartButtonClickEventArgs(number, type));
    }

    public DartBackgroundButtonControl(DartNumbers buttonNumber, int rotation, bool isButtonDark)
    {
        RenderTransform = new RotateTransform() { Angle = rotation };
        TextAngle = -rotation;
        this.buttonNumber = buttonNumber;
        ButtonNumber = (int)buttonNumber;
        IsButtonDark = isButtonDark;
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        BackgroundButtonElement = e.NameScope.Find("BackgroundButtonElement") as Button;
        DartDoubleButtonElement = e.NameScope.Find("DartDoubleButtonElement") as Button;
        DartTrippleButtonElement = e.NameScope.Find("DartTrippleButtonElement") as Button;
        TextBlock? buttonNumberText = e.NameScope.Find("ButtonNumberText") as TextBlock;

        if (buttonNumberText is not null)
        {
            buttonNumberText.PointerPressed += (o, args) => OnDartButtonClick(DartNumbers.Miss, DartsNumberModifier.Single);
        }

    }

    private void DartBackgroundButton_Click(object? sender, RoutedEventArgs args)
    {
        OnDartButtonClick(buttonNumber, DartsNumberModifier.Single);
    }

    private void DartDoubleButton_Click(object? sender, RoutedEventArgs args)
    {
        OnDartButtonClick(buttonNumber, DartsNumberModifier.Double);
    }

    private void DartTrippleButtonElement_Click(object? sender, RoutedEventArgs e)
    {
        OnDartButtonClick(buttonNumber, DartsNumberModifier.Triple);
    }
}
