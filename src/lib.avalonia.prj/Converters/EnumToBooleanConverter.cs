using Avalonia.Data;
using Avalonia.Data.Converters;
using System; 
using System.Globalization; 

namespace Lib.Avalonia.Converters
{
    /// <summary>
    /// Converter, преобразующий Enum => Boolean.
    /// ConverterParameter={x:Static enum:MyEnum.SomeEnumValue} => value?.Equals(parameter) == true.
    /// </summary>
    public class EnumToBooleanConverter : IValueConverter
    {
        public static readonly EnumToBooleanConverter Instance = new();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.Equals(parameter) == true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is bool isSelected && isSelected)
                return Enum.Parse(targetType, parameter.ToString());
            return BindingOperations.DoNothing;
        }
    }
}
