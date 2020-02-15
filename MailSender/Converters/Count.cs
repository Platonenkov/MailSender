using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [ValueConversion(typeof(IEnumerable), typeof(int)), MarkupExtensionReturnType(typeof(Count))]
    public class Count : ValueConverter
    {
        /// <inheritdoc />
        public override object Convert(object v, Type t, object p, CultureInfo c) => v is IEnumerable @enum ? @enum.Cast<object>().Count() : (object) null;
    }
}
