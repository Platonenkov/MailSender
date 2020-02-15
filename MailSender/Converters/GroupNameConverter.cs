using System;
using System.Globalization;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [MarkupExtensionReturnType(typeof(GroupNameConverter))]
    public class GroupNameConverter : ValueConverter
    {
        /// <inheritdoc />
        public override object Convert(object v, Type t, object p, CultureInfo c) => null;

        /// <inheritdoc />
        public override object ConvertBack(object v, Type t, object p, CultureInfo c)
        {
            if (v is null) return null;
            dynamic group = v;
            return (string) group.Name;
        }
    }
}
