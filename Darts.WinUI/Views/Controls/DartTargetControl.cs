using Darts.WinUI.Enums;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Linq;
using System.Windows.Input;

namespace Darts.WinUI.Views.Controls
{
    [TemplatePart(Name = DART_TARGET_CANVAS_NAME, Type = typeof(Grid))]
    public class DartTargetControl : Control
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

        public static DependencyProperty ClickCommandProperty = DependencyProperty.Register(
           nameof(ClickCommand),
           typeof(ICommand),
           typeof(DartTargetControl),
           new PropertyMetadata(null));

        public ICommand ClickCommand
        {
            get => (ICommand)GetValue(ClickCommandProperty);
            set => SetValue(ClickCommandProperty, value);
        }

        public DartTargetControl() 
        {
            this.DefaultStyleKey = typeof(DartTargetControl);
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

        protected override void OnApplyTemplate()
        {
            Grid background = GetTemplateChild(DART_TARGET_CANVAS_NAME) as Grid;
            if (background != null)
            {
                foreach (var item in dartNumberOrder
                    .Select((num, orderNum) => new { ButtonNumber = num, ButtonAngle = orderNum * BUTTON_ANGLE, IsButtonDark = !(orderNum % 2 == 1) }))
                {
                    var button = new DartBackgroundButtonControl(
                        item.ButtonNumber,
                        item.ButtonAngle,
                        item.IsButtonDark)
                    {
                        HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Center,
                        VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Center,
                    };

                    button.DartButtonClick += (sender, e) =>
                    {
                        OnDartButtonClick(e.Number, e.Type);
                    };

                    background.Children.Add(button);
                }
            }

            Button bullsEye = GetTemplateChild("SingleBullsEye") as Button;
            if (bullsEye != null)
            { 
                bullsEye.Click += (sender, e) => OnDartButtonClick(DartNumbers.BullsEye, DartsNumberType.Single );
            }

            Button doubleBullsEye = GetTemplateChild("DoubleBullsEye") as Button;
            if (doubleBullsEye != null)
            { 
                doubleBullsEye.Click += (sender, e) => OnDartButtonClick(DartNumbers.BullsEye, DartsNumberType.Double);
            }

            Button missButton = GetTemplateChild("DartMissButton") as Button;
            if (missButton != null)
            {
                missButton.Click += (sender, e) => OnDartButtonClick(DartNumbers.Miss, DartsNumberType.Single);
            }
        }
    }
}
