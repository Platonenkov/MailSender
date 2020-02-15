using System;
using System.Globalization;
using System.Linq;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [MarkupExtensionReturnType(typeof(OrMulty))]
    public class OrMulty : MultiValueConverter
    {
        public override object Convert(object[] vv, Type t, object p, CultureInfo c) =>
            !(vv is null) 
            && vv.Length != 0 
            && vv[0] is bool 
            && vv.OfType<bool>().Aggregate(false, (P, v) => P || v);

        public override object[] ConvertBack(object v, Type[] tt, object p, CultureInfo c) => 
            Enumerable
               .Repeat(v, tt.Length)
               .ToArray();
    }
}