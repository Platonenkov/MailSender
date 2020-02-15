using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [ValueConversion(typeof(string), typeof(bool)), MarkupExtensionReturnType(typeof(StringIsNull))]
    public class StringIsNull : ValueConverter
    {
        /// <inheritdoc />
        public override object Convert(object v, Type t, object p, CultureInfo c) => string.IsNullOrEmpty(v is string str ? str : v?.ToString());
    }
}