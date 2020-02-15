using System;
using System.Globalization;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [MarkupExtensionReturnType(typeof(InlineConverter))]
    public class InlineConverter : ValueConverter, IInlineConverter
    {
        public EventHandler<ConverterEventArgs> Converting { get; set; }
        public EventHandler<ConverterEventArgs> ConvertingBack { get; set; }

        public override object Convert(object v, Type t, object p, CultureInfo c)
        {
            var handlers = Converting;
            if(handlers is null) return null;
            var e = new ConverterEventArgs(v, t, p, c);
            handlers.Invoke(this, e);
            return e.ConvertedValue;
        }

        public override object ConvertBack(object v, Type t, object p, CultureInfo c)
        {
            var handlers = ConvertingBack;
            if(handlers is null) return null;
            var e = new ConverterEventArgs(v, t, p, c);
            handlers.Invoke(this, e);
            return e;
        }
    }
}