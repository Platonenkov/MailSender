using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [ValueConversion(typeof(bool), typeof(bool)), MarkupExtensionReturnType(typeof(Not))]
    public class Not : ValueConverter
    {
        public override object Convert(object v, Type t, object p, CultureInfo c) => !(v is bool b ? b : System.Convert.ToBoolean(v, c));

        public override object ConvertBack(object v, Type t, object p, CultureInfo c) => !(v is bool b ? b : System.Convert.ToBoolean(v, c));
    }
}