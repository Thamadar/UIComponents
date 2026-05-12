using Lib.Avalonia.Extensions;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

using System;
using System.Collections.Generic; 

namespace Lib.Avalonia.Controls
{
    /// <summary>
    /// Окружность для графики.
    /// </summary>
    public class CircleControl : Control
    { 

        public static readonly StyledProperty<IBrush> FillProperty =
       AvaloniaProperty.Register<CircleControl, IBrush>(nameof(Fill), defaultValue:Brushes.Transparent);

        public static readonly StyledProperty<IBrush> BorderBrushProperty =
       AvaloniaProperty.Register<CircleControl, IBrush>(nameof(BorderBrush), defaultValue:Brushes.Red);

        public static readonly StyledProperty<double> BorderThicknessProperty =
       AvaloniaProperty.Register<CircleControl, double>(nameof(BorderThickness),defaultValue: 1);

        public static readonly StyledProperty<double> RadiusProperty =
       AvaloniaProperty.Register<CircleControl, double>(nameof(Radius), defaultValue: 25);

        public static readonly StyledProperty<bool> IsSelectedProperty =
       AvaloniaProperty.Register<CircleControl, bool>(nameof(IsSelected));

        /// <summary>
        /// Заливка окружности.
        /// </summary>
        public IBrush Fill
        {
            get => GetValue(FillProperty);
            set => SetValue(FillProperty, value);
        }

        /// <summary>
        /// Заливка границы окружности.
        /// </summary>
        public IBrush BorderBrush
        {
            get => GetValue(BorderBrushProperty);
            set => SetValue(BorderBrushProperty, value);
        }
         
        /// <summary>
        /// Выбран ли элемент?
        /// </summary>
        public bool IsSelected
        {
            get => GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        /// <summary>
        /// Толщина границы окружности.
        /// </summary>
        public double BorderThickness
        {
            get => GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }
         
        /// <summary>
        /// Радиус окружности.
        /// </summary>
        public double Radius
        {
            get => GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
        }

        static CircleControl()
        { 
            AffectsRender<CircleControl>(RadiusProperty, IsSelectedProperty, FillProperty, BorderBrushProperty, BorderThicknessProperty);
        }
         
        public override void Render(DrawingContext context)
        { 
            var pen = new Pen(BorderBrush, BorderThickness);
            context.DrawEllipse(Fill, pen, Bounds.Center, Radius, Radius);
             
            if(IsSelected)
            {
                var rect = new Rect(
                    Bounds.Center.X - Radius - BorderThickness / 2 - 2,
                    Bounds.Center.Y - Radius - BorderThickness / 2 - 2,
                    Radius * 2 + BorderThickness + 4,
                    Radius * 2 + BorderThickness + 4);

                var selectPen = new Pen(Brushes.Black, 2)
                {
                    LineCap =  PenLineCap.Round,
                    DashStyle = new DashStyle(new double[] { 5, 5 }, 0)
                };
                context.DrawRectangle(selectPen, rect);
            }
        } 
    }
}
