using Avalonia;
using Avalonia.Controls;

namespace Darts.Avalonia.Controls.DartControl;

public class DartButtonControl : Button
{
    public static readonly StyledProperty<bool> IsButtonDarkProperty = AvaloniaProperty.Register<DartButtonControl, bool>(nameof(IsButtonDark));
    public bool IsButtonDark
    {
        get => GetValue(IsButtonDarkProperty);
        set => SetValue(IsButtonDarkProperty, value);
    }  
}
