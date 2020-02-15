using System.Linq;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [MarkupExtensionReturnType(typeof(Sum))]
    public class Sum : MultiDoubleConverter
    {
        protected override double To(double[] vv, object p) => vv?.Sum() ?? double.NaN;
    }
}