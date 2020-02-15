using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using MathCore.Annotations;

namespace MailSender.Converters
{
    [MarkupExtensionReturnType(typeof(CombineMulti))]
    public class CombineMulti : MultiValueConverter
    {
        [NotNull]
        public IMultiValueConverter First { get; set; }

        [CanBeNull]
        public IValueConverter Then { get; set; }

        public override object Convert(object[] vv, Type t, object p, CultureInfo c)
        {
            var result = First.Convert(vv, t, p, c);
            var then = Then;
            if (then != null) result = then.Convert(result, t, p, c);
            return result;
        }

        public override object[] ConvertBack(object v, Type[] tt, object p, CultureInfo c)
        {
            if (Then != null) v = Then.ConvertBack(v, v != null ? v.GetType() : typeof(object), p, c);
            return First.ConvertBack(v, tt, p, c);
        }
    }
}