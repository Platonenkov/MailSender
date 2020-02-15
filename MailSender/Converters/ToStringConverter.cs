using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [ValueConversion(typeof(object), typeof(string)), MarkupExtensionReturnType(typeof(ToStringConverter))]
    public class ToStringConverter : ValueConverter
    {
        public override object Convert(object v, Type t, object p, CultureInfo c) => v?.ToString();

        public override object ConvertBack(object v, Type t, object p, CultureInfo c) => v is null ? null : System.Convert.ChangeType(v, t, c);
    }
}
