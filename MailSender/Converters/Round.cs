using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [ValueConversion(typeof(double), typeof(double)), MarkupExtensionReturnType(typeof(Values2Point))]
    public class Round : DoubleValueConverter
    {
        public int Digits { get; set; }

        public MidpointRounding Rounding { get; set; }

        protected override double To(double v, object p) => Math.Round(v, Digits, Rounding);
        public override object ConvertBack(object v, Type t, object p, CultureInfo c) => v;
    }
}