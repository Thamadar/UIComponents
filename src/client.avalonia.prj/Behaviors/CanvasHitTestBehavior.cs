using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Xaml.Interactivity;
 
using Lib.Avalonia.Controls.Helpers; 
 
using System.Windows.Input;

namespace Client.Avalonia.Behaviors
{
    public class CanvasHitTestBehavior : Behavior<Canvas>
    {  
        public static readonly StyledProperty<bool> IsHitTestEnabledProperty =
            AvaloniaProperty.Register<CanvasHitTestBehavior, bool>(nameof(IsHitTestEnabled));

        public static readonly StyledProperty<ICommand?> PressCanvasCommandProperty =
            AvaloniaProperty.Register<CanvasHitTestBehavior, ICommand?>(nameof(PressCanvasCommand));

        /// <summary>
        /// Нажатие по Canvas.
        /// </summary>
        public ICommand? PressCanvasCommand
        {
            get => GetValue(PressCanvasCommandProperty);
            set => SetValue(PressCanvasCommandProperty, value);
        }

        public bool IsHitTestEnabled
        {
            get => GetValue(IsHitTestEnabledProperty);
            set => SetValue(IsHitTestEnabledProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            
            AssociatedObject?.AddHandler(Canvas.PointerPressedEvent, OnCanvasPointerPressed, RoutingStrategies.Tunnel);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject?.RemoveHandler(Canvas.PointerPressedEvent, OnCanvasPointerPressed); 
        } 

        private void OnCanvasPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if(AssociatedObject != null && 
               IsHitTestEnabled)
            {
                var point      = e.GetPosition(AssociatedObject); 
                var hitElement = AssociatedObject.InputHitTest(point) as Control;

                PressCanvasCommand?.Execute(new PointerHitInfo(hitElement, point));  
            } 
        } 
    }
}
