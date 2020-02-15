using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [ValueConversion(typeof(double), typeof(double)), MarkupExtensionReturnType(typeof(Abs))]
    public class Abs : DoubleValueConverter
    {
        protected override double To(double v, object p) => Math.Abs(v);

        public override object ConvertBack(object v, Type t, object p, CultureInfo c) => v;
    }
}