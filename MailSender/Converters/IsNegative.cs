using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [ValueConversion(typeof(double), typeof(bool)), MarkupExtensionReturnType(typeof(IsNegative))]
    public class IsNegative : ValueConverter
    {
        public bool Strong { get; set; }

        public IsNegative() => Strong = true;
            
        public override object Convert(object v, Type t, object p, CultureInfo c)
        {
            var value = DoubleValueConverter.ToDouble(v);
            return double.IsNaN(value) ? (object)false : Strong ? value < 0 : value <= 0;
        }
    }
}