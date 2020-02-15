using System;
using System.Globalization;
using System.Linq;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [MarkupExtensionReturnType(typeof(MinValuesConverter))]
    public class MinValuesConverter : MultiValueConverter
    {
        public override object Convert(object[] vv, Type t, object p, CultureInfo c) => vv?.Min();
    }
}