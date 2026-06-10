using Avalonia;
using Avalonia.Controls;
using Avalonia.Xaml.Interactivity;
using Lib.Avalonia.Extensions; 
using System.Linq; 
using Avalonia.Interactivity;

namespace Lib.Avalonia.Behaviors
{
    /// <summary>
    /// Behavior для произвдеения фокуса по имени (control.Name="") любого типа по завершению Load того контрола,
    /// в котором подключен данный Behavior.  
    /// Если FocusControlName пуст/не задан, то фокус устанавливается на объекте подключения.
    /// Также объект фокуса должен иметь значения свойства Focusable = true.
    /// </summary>
    public class FocusElementByNameBehavior : Behavior<Control>
    {
        public static readonly StyledProperty<string?> FocusControlNameProperty =
        AvaloniaProperty.Register<FocusElementByNameBehavior, string?>(nameof(FocusControlName));

        public static readonly StyledProperty<bool> IsTextBoxFocusControlProperty =
            AvaloniaProperty.Register<FocusElementByNameBehavior, bool>(nameof(IsTextBoxFocusControl));

        /// <summary>
        /// Наименование выбранного контрола, его свойство Name.
        /// </summary>
        public string? FocusControlName
        {
            get => GetValue(FocusControlNameProperty);
            set
            {
                if(value != null)
                {
                    SetValue(FocusControlNameProperty, value);
                }
            }
        }

        /// <summary>
        /// Является контрол для фокусом типа TextBox?
        /// Если да, то при фокусе выделится целиком текст.
        /// </summary>
        public bool IsTextBoxFocusControl
        {
            get => GetValue(IsTextBoxFocusControlProperty);
            set => SetValue(IsTextBoxFocusControlProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            if(AssociatedObject != null)
            {
                AssociatedObject.Loaded += OnAssociatedObjectLoaded;
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            if(AssociatedObject != null)
            {
                AssociatedObject.Loaded -= OnAssociatedObjectLoaded;
            }
        }

        private void OnAssociatedObjectLoaded(object? sender, RoutedEventArgs e)
        {
            if(AssociatedObject != null)
            {
                var childs = VisualExtension.FindDescendants<Control>(AssociatedObject);
                var focusControl = FocusControlName == null ? 
                                   AssociatedObject :
                                   childs.Where(x => x.Name == FocusControlName).FirstOrDefault();
                focusControl?.Focus();

                if(IsTextBoxFocusControl)
                { 
                    if(focusControl is TextBox textBoxControl)
                    {
                        textBoxControl?.SelectAll();
                    }
                }
            }
        }
    }
}
