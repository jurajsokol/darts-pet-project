using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Linq;

namespace Darts.WinUI.Views.Controls
{
    public class DartTargetControl : Control
    {
        private const string DART_TARGET_CANVAS_NAME = "BackgroundButtonsCanvas";

        private int[] dartNumberOrder = new[] { 20, 1, 18, 4, 13, 6, 10, 15, 2, 17, 3, 19, 7, 16, 8, 11, 14, 9, 12, 5 };
        private const int BUTTON_ANGLE = 18;

        public DartTargetControl() 
        {
            this.DefaultStyleKey = typeof(DartTargetControl);
        }

        protected override void OnApplyTemplate()
        {
            Grid background = GetTemplateChild(DART_TARGET_CANVAS_NAME) as Grid;
            if (background != null)
            {
                foreach (var item in dartNumberOrder
                    .Select((num, orderNum) => new { ButtonNumber = num, ButtonAngle = orderNum * BUTTON_ANGLE, IsButtonDark = !(orderNum % 2 == 1) }))
                {
                    background.Children.Add(new DartBackgroundButtonControl(
                        item.ButtonNumber,
                        item.ButtonAngle,
                        item.IsButtonDark)
                    { 
                        HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Center,
                        VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Center,
                    });
                }
            }
        }
    }
}
