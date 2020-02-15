using System.Windows.Markup;

namespace MailSender.Converters
{
    [MarkupExtensionReturnType(typeof(Subtract))]
    public class Subtract : ParameterMathConverter
    {
        public Subtract() : this(0) { }
        public Subtract(double value) : base(value, (v, p) => v - p, (v, p) => v + p) { }
    }
}