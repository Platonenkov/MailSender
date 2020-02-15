using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [ValueConversion(typeof(double), typeof(double)), MarkupExtensionReturnType(typeof(Multiply))]
    public class Multiply : ParameterMathConverter
    {
        public Multiply() : this(1) { }
        public Multiply(double K) : base(K, (v, x) => v * x, (r, x) => r / x) { }
    }
}