using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Darts.WinUI.Views.Controls
{
    //[TemplatePart(Name = "BackgroundButtonElement", Type = typeof(Button))]
    //[TemplatePart(Name = "DartDoubleButtonElement", Type = typeof(Button))]
    //[TemplatePart(Name = "DartTrippleButtonElement", Type = typeof(Button))]
    public class DartBackgroundButtonControl : Button
    {
        public int ButtonAngle { get; }
        public SolidColorBrush BackgroundColor { get; }
        public SolidColorBrush ForegroundColor { get; }

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

        public DartBackgroundButtonControl(int buttonNumber, int rotation, bool isButtonDark)
        {
            this.DefaultStyleKey = typeof(DartBackgroundButtonControl);
            RenderTransform = new RotateTransform() { Angle = rotation };
            TextAngle = -rotation;
            ButtonNumber = buttonNumber;
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
        }

    }
}
