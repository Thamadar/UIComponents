using Avalonia.Media; 
using System.Globalization; 

namespace Lib.Avalonia.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// Вычислить ширину строки текста в пикселях.
        /// </summary>
        /// <param name="text">текст для измерения</param>
        /// <param name="fontSize">размер шрифта</param>
        /// <param name="fontFamily">шрифт</param>
        /// <param name="fontWeight">жирность</param>
        /// <param name="fontStyle">курсив</param>
        public static double GetTextWidth(this string text, double fontSize,
            FontFamily? fontFamily = null,
            FontWeight? fontWeight = null,
            FontStyle? fontStyle = null)
        {
            if(string.IsNullOrEmpty(text))
                return 0;
             
            var formattedText = new FormattedText(
                text,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(fontFamily ?? "Default", fontStyle ?? FontStyle.Normal, fontWeight ?? FontWeight.Normal),
                fontSize,
                Brushes.Black
            );

            return formattedText.Width;
        }
    }
}
