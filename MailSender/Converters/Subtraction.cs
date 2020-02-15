using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [ValueConversion(typeof(double), typeof(double)), MarkupExtensionReturnType(typeof(Subtraction))]
    public class Subtraction : ParameterMathConverter
    {
        public Subtraction() : this(0) { }
        public Subtraction(double p) : base(p, (v, x) => v - x, (r, x) => r + x) { }
    }
}