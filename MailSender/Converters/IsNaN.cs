using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [ValueConversion(typeof(double), typeof(bool)), MarkupExtensionReturnType(typeof(IsNaN))]
    public class IsNaN : ValueConverter
    {
        public override object Convert(object v, Type t, object p, CultureInfo c) => double.IsNaN(DoubleValueConverter.ToDouble(v));
    }
}