using Darts.WinUI.Enums;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using Windows.Devices.Geolocation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Darts.WinUI.Views.Controls
{
    [TemplatePart(Name = "BackgroundButtonElement", Type = typeof(Button))]
    [TemplatePart(Name = "DartDoubleButtonElement", Type = typeof(Button))]
    [TemplatePart(Name = "DartTrippleButtonElement", Type = typeof(Button))]
    public class DartBackgroundButtonControl : Button
    {
        private DartNumbers buttonNumber;

        public int ButtonAngle { get; }

        private Button backgroundButtonElement;
        private Button BackgroundButtonElement
        {
            get => backgroundButtonElement;

            set
            {
                if (backgroundButtonElement != null)
                {
                    backgroundButtonElement.Click -= new RoutedEventHandler(OnBackgroundButtonClick);
                }
                backgroundButtonElement = value;

                if (backgroundButtonElement != null)
                {
                    backgroundButtonElement.Click += new RoutedEventHandler(OnBackgroundButtonClick);
                }
            }
        }

        private Button dartDoubleButtonElement;
        private Button DartDoubleButtonElement
        {
            get => dartDoubleButtonElement;

            set
            {
                if (dartDoubleButtonElement != null)
                {
                    dartDoubleButtonElement.Click -= new RoutedEventHandler(OnDoubleButtonClick);
                }
                dartDoubleButtonElement = value;

                if (dartDoubleButtonElement != null)
                {
                    dartDoubleButtonElement.Click += new RoutedEventHandler(OnDoubleButtonClick);
                }
            }
        }

        private Button dartTrippleButtonElement;
        private Button DartTrippleButtonElement
        {
            get => dartTrippleButtonElement;

            set
            {
                if (dartTrippleButtonElement != null)
                {
                    dartTrippleButtonElement.Click -= new RoutedEventHandler(OnTrippleButtonClick);
                }
                dartTrippleButtonElement = value;

                if (dartTrippleButtonElement != null)
                {
                    dartTrippleButtonElement.Click += new RoutedEventHandler(OnTrippleButtonClick);
                }
            }
        }

        public static readonly Microsoft.UI.Xaml.DependencyProperty TextAngleProperty = Microsoft.UI.Xaml.DependencyProperty.Register(
              name: "TextAngle",
              propertyType: typeof(int),
              ownerType: typeof(DartBackgroundButtonControl),
              typeMetadata: new Microsoft.UI.Xaml.PropertyMetadata(0)
            );

        public int TextAngle
        {
            get => (int)GetValue(TextAngleProperty);
            set => SetValue(TextAngleProperty, value);
        }

        public static readonly Microsoft.UI.Xaml.DependencyProperty IsButtonDarkProperty = Microsoft.UI.Xaml.DependencyProperty.Register(
              name: nameof(IsButtonDark),
              propertyType: typeof(bool),
              ownerType: typeof(DartBackgroundButtonControl),
              typeMetadata: new Microsoft.UI.Xaml.PropertyMetadata(0)
            );

        public bool IsButtonDark
        {
            get => (bool)GetValue(IsButtonDarkProperty);
            set => SetValue(IsButtonDarkProperty, value);
        }

        public static readonly Microsoft.UI.Xaml.DependencyProperty ButtonNumberProperty = Microsoft.UI.Xaml.DependencyProperty.Register(
              name: "ButtonNumber",
              propertyType: typeof(int),
              ownerType: typeof(DartBackgroundButtonControl),
              typeMetadata: new Microsoft.UI.Xaml.PropertyMetadata(0)
            );

        public int ButtonNumber
        {
            get => (int)GetValue(ButtonNumberProperty);
            set => SetValue(ButtonNumberProperty, value);
        }

        public event EventHandler<DartButtonClickEventArgs> DartButtonClick;

        protected void OnDartButtonClick(DartNumbers number, DartsNumberType type)
        {
            DartButtonClick?.Invoke(this, new DartButtonClickEventArgs(number, type));
        }

        public DartBackgroundButtonControl(DartNumbers buttonNumber, int rotation, bool isButtonDark)
        {
            this.DefaultStyleKey = typeof(DartBackgroundButtonControl);
            RenderTransform = new RotateTransform() { Angle = rotation };
            TextAngle = -rotation;
            this.buttonNumber = buttonNumber;
            ButtonNumber = (int) buttonNumber;
            IsButtonDark = isButtonDark;
        }

        private void UpdateStates(bool useTransitions)
        {
            if (IsButtonDark)
            {
                VisualStateManager.GoToState(this, "LightButtonStyle", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, "DarkButtonStyle", useTransitions);
            }
        }

        protected override void OnApplyTemplate()
        {
            UpdateStates(false);
            BackgroundButtonElement = GetTemplateChild("BackgroundButtonElement") as Button;
            DartDoubleButtonElement = GetTemplateChild("DartDoubleButtonElement") as Button;
            DartTrippleButtonElement = GetTemplateChild("DartTrippleButtonElement") as Button;
            TextBlock buttonNumberText = GetTemplateChild("ButtonNumberText") as TextBlock;

            if (buttonNumberText != null)
            {
                buttonNumberText.PointerPressed += (o, args) => OnDartButtonClick(DartNumbers.Miss, DartsNumberType.Single);
            }

            base.OnApplyTemplate();
        }

        private void OnBackgroundButtonClick(object sender, RoutedEventArgs args)
        {
            OnDartButtonClick(buttonNumber, DartsNumberType.Single);
        }

        private void OnDoubleButtonClick(object sender, RoutedEventArgs args)
        {
            OnDartButtonClick(buttonNumber, DartsNumberType.Double);
        }

        private void OnTrippleButtonClick(object sender, RoutedEventArgs args)
        {
            OnDartButtonClick(buttonNumber, DartsNumberType.Tripple);
        }
    }
}
