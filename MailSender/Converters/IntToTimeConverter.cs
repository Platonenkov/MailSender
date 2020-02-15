using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [ValueConversion(typeof(int), typeof(string)), MarkupExtensionReturnType(typeof(IntToTimeConverter))]
    public class IntToTimeConverter : ValueConverter
    {
        public override object ProvideValue(IServiceProvider sp) => this;

        public override object Convert(object v, Type t, object p, CultureInfo c) => !(v is int time) ? "--:--:--" : $"{time / 3600,2}:{(time / 60) % 60,2:00}:{time % 60,2:00}";

        public override object ConvertBack(object v, Type t, object p, CultureInfo c)
        {
            if (!(v is string str)) return null;
            var parts = str.Split(':');
            return parts.Length != 3 ? 0 : int.Parse(parts[0]) * 3600 + int.Parse(parts[1]) * 60 + int.Parse(parts[2]);
        }
    }
}
