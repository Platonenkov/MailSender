using System;
using System.Globalization;
using System.Windows.Data;

namespace MailSender.Converters
{
    [ValueConversion(typeof(double), typeof(string))]
    public class DummyBool_Converter : IValueConverter
    {
        public object Convert(object value, Type t, object p, CultureInfo culture) => !(bool)value;

        public object ConvertBack(object value, Type t, object p, CultureInfo culture) => !(bool)value;
    }

}
