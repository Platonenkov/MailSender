using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [ValueConversion(typeof(object), typeof(Type)), MarkupExtensionReturnType(typeof(GetType))]
    public class GetType : ValueConverter
    {
        /// <inheritdoc />
        public override object Convert(object v, Type t, object p, CultureInfo c) => v?.GetType();
    }
}