using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [ValueConversion(typeof(double), typeof(bool)), MarkupExtensionReturnType(typeof(GreateThen))]
    public class GreateThen : ValueConverter
    {
        public double Value { get; set; }

        public GreateThen() { }

        public GreateThen(double value) => Value = value;

        public override object Convert(object v, Type t, object p, CultureInfo c)
        {
            var value = DoubleValueConverter.ToDouble(v);
            return double.IsNaN(value) ? (object)false : value > Value;
        }
    }
}