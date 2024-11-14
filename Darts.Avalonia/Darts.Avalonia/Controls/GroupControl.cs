using Avalonia;
using Avalonia.Controls;

namespace Darts.Avalonia.Controls;

public class GroupControl : ContentControl
{
    public static readonly StyledProperty<string> HeaderTextProperty = AvaloniaProperty
        .Register<GroupControl, string>(nameof(HeaderText), defaultValue: string.Empty);

    public string HeaderText
    {
        get => GetValue(HeaderTextProperty);
        set => SetValue(HeaderTextProperty, value);
    }
}
