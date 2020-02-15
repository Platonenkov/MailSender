using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [ValueConversion(typeof(double), typeof(double)), MarkupExtensionReturnType(typeof(LessThen))]
    public class Reverse : ValueConverter
    {
        public Reverse() { }


        public override object Convert(object v, Type t, object p, CultureInfo c)
        {
            var value = DoubleValueConverter.ToDouble(v);
            return value*-1;
        }
    }
}