using Avalonia;
using Avalonia.Controls.Primitives; 

namespace Lib.Avalonia.Controls
{
    public class DialogDarkControl : TemplatedControl
    {
        public static readonly StyledProperty<bool> IsActiveProperty =
        AvaloniaProperty.Register<DialogDarkControl, bool>(nameof(IsActive));

        public bool IsActive
        {
            get => GetValue(IsActiveProperty);
            set => SetValue(IsActiveProperty, value);
        } 
    }
}
