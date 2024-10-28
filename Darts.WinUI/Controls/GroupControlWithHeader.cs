using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Darts.MVVM.Controls
{
    public class GroupControlWithHeader : ContentControl
    {
        public static readonly DependencyProperty HeaderTextProperty = DependencyProperty.Register(
              name: nameof(HeaderText),
              propertyType: typeof(string),
              ownerType: typeof(GroupControlWithHeader),
              typeMetadata: new PropertyMetadata(defaultValue: string.Empty));

        public string HeaderText
        {
            get => (string)GetValue(HeaderTextProperty);
            set => SetValue(HeaderTextProperty, value);
        }
    }
}
