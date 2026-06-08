using Avalonia;
using Avalonia.Controls;
using System; 

namespace Lib.Avalonia.Controls.Helpers
{
    public class PointerHitInfo
    {
        public Control? HitControl { get; }
        public Point Point { get; }

        public PointerHitInfo(Control? hitControl, Point point)
        {
            HitControl = hitControl;
            Point      = point;
        }
    }
}
