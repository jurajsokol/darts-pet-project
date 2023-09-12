using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Darts.WinUI.Views.Controls
{
    public class DartButtonControl : Button
    {
        public static readonly Microsoft.UI.Xaml.DependencyProperty IsButtonDarkProperty = Microsoft.UI.Xaml.DependencyProperty.Register(
              name: nameof(IsButtonDark),
              propertyType: typeof(bool),
              ownerType: typeof(DartButtonControl),
              typeMetadata: new Microsoft.UI.Xaml.PropertyMetadata(false, (o, args) => (o as DartButtonControl).UpdateStates(false)));

        public bool IsButtonDark
        {
            get => (bool)GetValue(IsButtonDarkProperty);
            set => SetValue(IsButtonDarkProperty, value);
        }

        public DartButtonControl()
        {
            this.DefaultStyleKey = typeof(DartButtonControl);
        }

        protected override void OnApplyTemplate()
        {
            UpdateStates(false);
        }

        private void UpdateStates(bool useTransitions)
        {
            if (IsButtonDark)
            {
                VisualStateManager.GoToState(this, "DarkButtonStyle", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, "LightButtonStyle", useTransitions);
            }
        }
    }
}
