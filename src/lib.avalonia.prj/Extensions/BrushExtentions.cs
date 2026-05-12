using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Avalonia.Extensions
{
    public static class BrushExtentions
    {
        public static SolidColorBrush? ToSolidColorBrush(this IBrush brush, double opacity = 1)
        {
            if(brush is SolidColorBrush solid)
            {
                return new SolidColorBrush(Color.FromArgb((byte)(255 * opacity), solid.Color.R, solid.Color.G, solid.Color.B)); 
            }

            return null;
        }
    }
}
