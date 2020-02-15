using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [ValueConversion(typeof(double), typeof(string)), MarkupExtensionReturnType(typeof(DurationToString_Converter))]
    public class DurationToString_Converter : ValueConverter
    {
        #region IValueConverter Members

        public override object Convert(object v, Type t, object p, CultureInfo c)
        {
            var duration = DoubleValueConverter.ToDouble(v, c);
            var cs = (int)((duration - (int)duration) * 100);
            var sec = ((int)duration % 60);
            var min = ((int)duration / 60) % 60;
            var hr = (int)duration / 3600;

            return $"{hr:00}:{min:00}:{sec:00}.{cs:00}";
        }

        #endregion
    }
}
