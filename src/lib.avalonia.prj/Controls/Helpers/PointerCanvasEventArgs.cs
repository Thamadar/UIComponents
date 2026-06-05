using Avalonia;
using Avalonia.Controls;
using System; 

namespace Lib.Avalonia.Controls.Helpers
{
    public class PointerCanvasEventArgs : EventArgs
    {
        public Control HitControl { get; }
        public Point Point { get; }

        public PointerCanvasEventArgs(Control hitControl, Point point)
        {
            HitControl = hitControl;
            Point      = point;
        }
    }
}
