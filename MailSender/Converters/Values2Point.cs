using System;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [MarkupExtensionReturnType(typeof(Values2Point))]
    public class Values2Point : MultiValueConverter
    {
        public override object ProvideValue(IServiceProvider sp) => this;

        public override object Convert(object[] vv, Type t, object p, CultureInfo c) => new Point((double)vv[0], (double)vv[1]);

        public override object[] ConvertBack(object v, Type[] tt, object p, CultureInfo c) => new object[] { ((Point)v).X, ((Point)v).Y };
    }
}