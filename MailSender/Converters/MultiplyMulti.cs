using System;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [MarkupExtensionReturnType(typeof(MultiplyMulti))]
    public class MultiplyMulti : MultiValueConverter
    {
        private static readonly object __UnsetValue = DependencyProperty.UnsetValue;

        public override object Convert(object[] vv, Type t, object p, CultureInfo c)
        {
            if (vv is null || vv.Length == 0) return double.NaN;
            var result = 1d;
            foreach (var v in vv)
            {
                if (ReferenceEquals(v, __UnsetValue)) return double.NaN;
                result *= v is double value ? value : System.Convert.ToDouble(v);
            }

            return result;
        }
    }
}