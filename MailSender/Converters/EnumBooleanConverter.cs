using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    /// <summary>
    /// конвертер из Enum в bool
    /// </summary>
    [ValueConversion(typeof(Enum), typeof(bool)), MarkupExtensionReturnType(typeof(EnumBooleanConverter))]

    public class EnumBooleanConverter : ValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null || parameter is null) return Binding.DoNothing;
            var result = value.ToString().Equals(parameter.ToString());
            return result;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            value is null || parameter is null ? Binding.DoNothing : value.Equals(true) ? parameter : Binding.DoNothing;
    }
}
