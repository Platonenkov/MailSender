using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [ValueConversion(typeof(double), typeof(bool)), MarkupExtensionReturnType(typeof(IsPositive))]
    public class IsPositive : ValueConverter
    {
        public bool Strong { get; set; }

        public IsPositive() => Strong = true;

        public override object Convert(object v, Type t, object p, CultureInfo c)
        {
            var value = DoubleValueConverter.ToDouble(v);
            return double.IsNaN(value) ? (object)false : Strong ? value > 0 : value >= 0;
        }
    }
}