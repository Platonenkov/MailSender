using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    public class AndMulty : MarkupExtension, IMultiValueConverter
    {
        public override object ProvideValue(IServiceProvider sp) => this;

        public object Convert(object[] vv, Type t, object p, CultureInfo c)
        {
            if (vv is null || vv.Length == 0 || !(vv[0] is bool)) return false;
            return vv.OfType<bool>().Aggregate(true, (P, v) => P && v);
        }

        public object[] ConvertBack(object v, Type[] tt, object p, CultureInfo c) => Enumerable.Repeat(v, tt.Length).ToArray();
    }
}