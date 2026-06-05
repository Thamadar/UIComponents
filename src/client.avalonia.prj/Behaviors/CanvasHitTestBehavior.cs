using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Xaml.Interactivity;

using Client.Avalonia.Services;
using Client.Avalonia.Views.Geometry.Shapes;

using Lib.Avalonia.Extensions;
 
using System.Windows.Input;

namespace Client.Avalonia.Behaviors
{
    public class CanvasHitTestBehavior : Behavior<Canvas>
    {
        private List<IDisposable> _disposables = new List<IDisposable>();

        public static readonly StyledProperty<bool> IsHitTestEnabledProperty =
            AvaloniaProperty.Register<CanvasHitTestBehavior, bool>(nameof(IsHitTestEnabled));

        public static readonly StyledProperty<ICommand?> CreateCommandProperty =
            AvaloniaProperty.Register<CanvasHitTestBehavior, ICommand?>(nameof(CreateCommand));

        /// <summary>
        /// 
        /// </summary>
        public ICommand? CreateCommand
        {
            get => GetValue(CreateCommandProperty);
            set => SetValue(CreateCommandProperty, value);
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
            _disposables.DisposeAll();
        } 

        private void OnCanvasPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if(AssociatedObject != null && 
               IsHitTestEnabled)
            {
                var point = e.GetPosition(AssociatedObject);

                var hitElement = AssociatedObject.InputHitTest(point) as Control;

                if(hitElement != null && hitElement != AssociatedObject)
                {
                    ShapePress(hitElement, point);
                }
                else
                {
                    CanvasPress(AssociatedObject, point);
                }

                e.Handled = true;
            } 
        }

        /// <summary>
        /// Реакция на нажатие по полотну Canvas.
        /// </summary> 
        private void CanvasPress(Canvas canvas, Point point)
        {
            CreateCommand?.Execute(point);
        }

        /// <summary>
        /// Реакция на нажатие по графическому элементу.
        /// </summary> 
        private void ShapePress(Control hitElement, Point point)
        {
            if(hitElement.DataContext is IShapeItem shapeItem)
            {
                ShapeService.Instance.SelectShapeById(shapeItem.Id);
            }
        }
    }
}
