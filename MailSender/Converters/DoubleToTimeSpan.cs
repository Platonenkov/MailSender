using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [ValueConversion(typeof(double), typeof(TimeSpan)), MarkupExtensionReturnType(typeof(DoubleToTimeSpan))]
    public class DoubleToTimeSpan : ValueConverter
    {
        public override object Convert(object v, Type t, object p, CultureInfo c) => v is null ? null : TimeSpan.FromSeconds((dynamic) v);

        public override object ConvertBack(object v, Type t, object p, CultureInfo c) => !(v is TimeSpan time) ? null : System.Convert.ChangeType(time.Seconds, t);
    }
}
