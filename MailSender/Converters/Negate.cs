using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [ValueConversion(typeof(double), typeof(bool)), MarkupExtensionReturnType(typeof(Negate))]
    public class Negate : DoubleValueConverter
    {
        protected override double To(double v, object p) => -v;

        protected override double From(double v, object p) => -v;
    }
}