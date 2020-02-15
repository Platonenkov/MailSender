using System;
using System.Windows.Controls;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [MarkupExtensionReturnType(typeof(BooleanToVisibilityConverter))]
    public class Bool2Visibility : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider sp) => new BooleanToVisibilityConverter();
    }
}