using System;
using System.Globalization;
using System.Windows.Markup;
using System.Windows.Media;

namespace MailSender.Converters
{
    [MarkupExtensionReturnType(typeof(ColorToBrushConverter))]
    public class ColorToBrushConverter : ValueConverter
    {
        public override object Convert(object v, Type t, object p, CultureInfo c) => v is Color color ? new SolidColorBrush(color) : null;

        public override object ConvertBack(object v, Type t, object p, CultureInfo c) => v is SolidColorBrush brush ? brush.Color : (object) null;
    }
}
