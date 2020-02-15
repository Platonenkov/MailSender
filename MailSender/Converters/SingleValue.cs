using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using MathCore.Annotations;

namespace MailSender.Converters
{
    [MarkupExtensionReturnType(typeof(SingleValue))]
    public class SingleValue : MultiValueConverter
    {
        [CanBeNull] public IValueConverter Next { get; set; }

        public override object Convert(object[] vv, Type t, object p, CultureInfo c)
        {
            var value = vv is null || vv.Length == 0 ? null : vv[0];
            return Next is null ? value : Next.Convert(value, t, p, c);
        }
    }
}