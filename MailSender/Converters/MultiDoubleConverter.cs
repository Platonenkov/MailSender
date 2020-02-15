using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [MarkupExtensionReturnType(typeof(MultiDoubleConverter))]
    public abstract class MultiDoubleConverter : MultiValueConverter
    {
        public override object Convert(object[] vv, Type t, object p, CultureInfo c) => To(vv?.ToArray(ConvertToDouble), p);

        private static double ConvertToDouble(object v) => v is double d 
            ? d 
            : v == DependencyProperty.UnsetValue 
                ? double.NaN 
                : System.Convert.ToDouble(v);

        public override object[] ConvertBack(object v, Type[] tt, object p, CultureInfo c) => From(v is double d ? d : System.Convert.ToDouble(v), p).Cast<object>().ToArray();

        protected abstract double To(double[] vv, object p);
        protected virtual double[] From(double v, object p) => throw new NotSupportedException();
    }
}